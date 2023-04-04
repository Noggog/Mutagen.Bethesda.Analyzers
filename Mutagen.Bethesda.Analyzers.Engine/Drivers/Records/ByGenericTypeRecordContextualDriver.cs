using Mutagen.Bethesda.Analyzers.SDK.Analyzers;
using Mutagen.Bethesda.Plugins.Records;

namespace Mutagen.Bethesda.Analyzers.Drivers.Records;

public class ByGenericTypeRecordContextualDriver<TMajor> : IContextualDriver
    where TMajor : class, IMajorRecordGetter
{
    private readonly IContextualRecordAnalyzer<TMajor>[] _contextualRecordAnalyzers;

    public bool Applicable => _contextualRecordAnalyzers.Length > 0;

    public IEnumerable<IAnalyzer> Analyzers => _contextualRecordAnalyzers;

    public ByGenericTypeRecordContextualDriver(
        IContextualRecordAnalyzer<TMajor>[] contextualRecordAnalyzers)
    {
        _contextualRecordAnalyzers = contextualRecordAnalyzers;
    }

    public void Drive(ContextualDriverParams driverParams)
    {
        foreach (var listing in driverParams.LoadOrder.ListedOrder)
        {
            if (listing.Mod == null) continue;

            foreach (var rec in listing.Mod.EnumerateMajorRecords<TMajor>())
            {
                foreach (var analyzer in _contextualRecordAnalyzers)
                {
                    var result = analyzer.AnalyzeRecord(new ContextualRecordAnalyzerParams<TMajor>(
                        driverParams.LinkCache,
                        driverParams.LoadOrder,
                        rec));
                    if (result == null) continue;
                    foreach (var topic in result.Topics)
                    {
                        driverParams.ReportDropbox.Dropoff(
                            listing.Mod,
                            rec,
                            topic);
                    }
                }
            }
        }
    }
}