using Mutagen.Bethesda.Analyzers.SDK.Analyzers;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Binary.Headers;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Plugins.RecordTypeMapping;

namespace Mutagen.Bethesda.Analyzers.Drivers.RecordFrame;

public class ByGenericTypeRecordFrameIsolatedDriver<TMajor> : IIsolatedRecordFrameAnalyzerDriver
    where TMajor : class, IMajorRecordGetter
{
    private readonly IIsolatedRecordFrameAnalyzer<TMajor>[] _isolatedRecordFrameAnalyzers;

    public bool Applicable => _isolatedRecordFrameAnalyzers.Length > 0;

    public IEnumerable<IAnalyzer> Analyzers => _isolatedRecordFrameAnalyzers;

    public RecordType TargetType => MajorRecordTypeLookup<TMajor>.RecordType;

    public ByGenericTypeRecordFrameIsolatedDriver(
        IIsolatedRecordFrameAnalyzer<TMajor>[] isolatedRecordFrameAnalyzers)
    {
        _isolatedRecordFrameAnalyzers = isolatedRecordFrameAnalyzers;
    }

    public void Drive(IsolatedDriverParams driverParams, MajorRecordFrame frame)
    {
        var param = new IsolatedRecordFrameAnalyzerParams<TMajor>(frame);

        foreach (var analyzer in _isolatedRecordFrameAnalyzers)
        {
            var result = analyzer.AnalyzeRecord(param);
            if (result == null) continue;
            foreach (var topic in result.Topics)
            {
                driverParams.ReportDropbox.Dropoff(topic);
            }
        }
    }
}