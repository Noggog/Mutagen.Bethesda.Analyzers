using System.Buffers;
using Mutagen.Bethesda.Analyzers.SDK.Analyzers;
using Mutagen.Bethesda.Environments.DI;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Analysis;
using Mutagen.Bethesda.Plugins.Binary.Headers;
using Mutagen.Bethesda.Plugins.Meta;
using Noggog;

namespace Mutagen.Bethesda.Analyzers.Drivers.RecordFrame;

public class RecordFrameDriver : IIsolatedDriver, IContextualDriver
{
    private readonly IGameReleaseContext _gameReleaseContext;
    private readonly IDataDirectoryProvider _dataDataDirectoryProvider;

    private class Drivers
    {
        public IIsolatedRecordFrameAnalyzerDriver[] Isolated = Array.Empty<IIsolatedRecordFrameAnalyzerDriver>();
        public IContextualRecordFrameAnalyzerDriver[] Contextual = Array.Empty<IContextualRecordFrameAnalyzerDriver>();
    }

    private readonly Dictionary<RecordType, Drivers> _mapping = new();
    public bool Applicable => _mapping.Count > 0;

    public IEnumerable<IAnalyzer> Analyzers => _mapping.Values.SelectMany(x =>
    {
        return x.Contextual.SelectMany(x => x.Analyzers)
            .Concat(x.Isolated.SelectMany(x => x.Analyzers));
    });

    public RecordFrameDriver(
        IGameReleaseContext gameReleaseContext,
        IDataDirectoryProvider dataDataDirectoryProvider,
        IIsolatedRecordFrameAnalyzerDriver[] isolatedDrivers,
        IContextualRecordFrameAnalyzerDriver[] contextualDrivers)
    {
        _gameReleaseContext = gameReleaseContext;
        _dataDataDirectoryProvider = dataDataDirectoryProvider;
        foreach (var drivers in isolatedDrivers
                     .Where(x => x.Applicable)
                     .GroupBy(x => x.TargetType))
        {
            _mapping.GetOrAdd(drivers.Key).Isolated = drivers.ToArray();
        }
        foreach (var drivers in contextualDrivers
                     .Where(x => x.Applicable)
                     .GroupBy(x => x.TargetType))
        {
            _mapping.GetOrAdd(drivers.Key).Contextual = drivers.ToArray();
        }
    }

    public void Drive(ContextualDriverParams driverParams)
    {
        foreach (var listing in driverParams.LoadOrder.ListedOrder)
        {
            if (listing.Mod == null) continue;

            var modPath = Path.Combine(_dataDataDirectoryProvider.Path, listing.ModKey.FileName);

            using var stream = File.OpenRead(modPath);

            var locs = RecordLocator.GetLocations(modPath, _gameReleaseContext.Release);

            foreach (var recordLocationMarker in locs.ListedRecords)
            {
                if (!_mapping.TryGetValue(recordLocationMarker.Value.Record, out var analyzerBundles)) continue;

                stream.Position = recordLocationMarker.Value.Location.Min;

                var width = checked((int)recordLocationMarker.Value.Location.Width);

                var buf = ArrayPool<byte>.Shared.Rent(width);

                try
                {
                    stream.Read(buf.AsSpan().Slice(0, width));

                    var frame = new MajorRecordFrame(GameConstants.Get(_gameReleaseContext.Release), buf);

                    if (analyzerBundles.Isolated.Length > 0)
                    {
                        var isolatedParams = new IsolatedDriverParams(
                            listing.Mod.ToUntypedImmutableLinkCache(),
                            driverParams.ReportDropbox,
                            listing.Mod,
                            modPath);

                        foreach (var analyzer in analyzerBundles.Isolated)
                        {
                            analyzer.Drive(isolatedParams, frame);
                        }
                    }

                    foreach (var analyzer in analyzerBundles.Contextual)
                    {
                        analyzer.Drive(driverParams, frame);
                    }
                }
                finally
                {
                    ArrayPool<byte>.Shared.Return(buf);
                }
            }
        }
    }

    public void Drive(IsolatedDriverParams driverParams)
    {
        using var stream = File.OpenRead(driverParams.TargetModPath);

        var locs = RecordLocator.GetLocations(driverParams.TargetModPath, _gameReleaseContext.Release);

        foreach (var recordLocationMarker in locs.ListedRecords)
        {
            if (!_mapping.TryGetValue(recordLocationMarker.Value.Record, out var analyzerBundles)) continue;

            stream.Position = recordLocationMarker.Value.Location.Min;

            var width = checked((int)recordLocationMarker.Value.Location.Width);

            var buf = ArrayPool<byte>.Shared.Rent(width);

            try
            {
                stream.Read(buf.AsSpan().Slice(0, width));

                var frame = new MajorRecordFrame(GameConstants.Get(_gameReleaseContext.Release), buf);

                if (analyzerBundles.Isolated.Length > 0)
                {
                    foreach (var analyzer in analyzerBundles.Isolated)
                    {
                        analyzer.Drive(driverParams, frame);
                    }
                }
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buf);
            }
        }
    }
}