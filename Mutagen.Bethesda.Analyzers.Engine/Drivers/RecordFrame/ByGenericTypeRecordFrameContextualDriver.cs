using Mutagen.Bethesda.Analyzers.SDK.Analyzers;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Binary.Headers;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Plugins.RecordTypeMapping;

namespace Mutagen.Bethesda.Analyzers.Drivers.RecordFrame;

public class ByGenericTypeRecordFrameContextualDriver<TMajor> : IContextualRecordFrameAnalyzerDriver
    where TMajor : class, IMajorRecordGetter
{
    private readonly IContextualRecordFrameAnalyzer<TMajor>[] _contextualRecordFrameAnalyzers;

    public bool Applicable => _contextualRecordFrameAnalyzers.Length > 0;

    public IEnumerable<IAnalyzer> Analyzers => _contextualRecordFrameAnalyzers;

    public RecordType TargetType => MajorRecordTypeLookup<TMajor>.RecordType;

    public ByGenericTypeRecordFrameContextualDriver(
        IContextualRecordFrameAnalyzer<TMajor>[] contextualRecordFrameAnalyzers)
    {
        _contextualRecordFrameAnalyzers = contextualRecordFrameAnalyzers;
    }

    public void Drive(ContextualDriverParams driverParams, MajorRecordFrame frame)
    {
        var param = new ContextualRecordFrameAnalyzerParams<TMajor>(
            driverParams.LinkCache,
            driverParams.LoadOrder,
            frame);

        foreach (var analyzer in _contextualRecordFrameAnalyzers)
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