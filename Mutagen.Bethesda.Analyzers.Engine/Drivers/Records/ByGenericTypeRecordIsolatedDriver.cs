using Mutagen.Bethesda.Analyzers.SDK.Analyzers;
using Mutagen.Bethesda.Plugins.Records;

namespace Mutagen.Bethesda.Analyzers.Drivers.Records;

public class ByGenericTypeRecordIsolatedDriver<TMajor> : IIsolatedDriver
    where TMajor : class, IMajorRecordGetter
{
    private readonly IIsolatedRecordAnalyzer<TMajor>[] _isolatedRecordAnalyzers;

    public bool Applicable => _isolatedRecordAnalyzers.Length > 0;

    public IEnumerable<IAnalyzer> Analyzers => _isolatedRecordAnalyzers;

    public ByGenericTypeRecordIsolatedDriver(
        IIsolatedRecordAnalyzer<TMajor>[] isolatedRecordAnalyzers)
    {
        _isolatedRecordAnalyzers = isolatedRecordAnalyzers;
    }

    public void Drive(IsolatedDriverParams driverParams)
    {
        foreach (var rec in driverParams.TargetMod.EnumerateMajorRecords<TMajor>())
        {
            var isolatedParam = new IsolatedRecordAnalyzerParams<TMajor>(rec);
            foreach (var analyzer in _isolatedRecordAnalyzers)
            {
                var record = analyzer.AnalyzeRecord(isolatedParam);
                if (record == null) continue;
                foreach (var topic in record.Topics)
                {
                    driverParams.ReportDropbox.Dropoff(
                        driverParams.TargetMod,
                        rec,
                        topic);
                }
            }
        }
    }
}