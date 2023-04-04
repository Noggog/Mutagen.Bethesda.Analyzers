using Mutagen.Bethesda.Analyzers.SDK.Analyzers;

namespace Mutagen.Bethesda.Analyzers.Drivers;

public class ContextualDriver : IContextualDriver
{
    private readonly IContextualAnalyzer[] _contextualAnalyzers;
    public bool Applicable => true;
    public IEnumerable<IAnalyzer> Analyzers => _contextualAnalyzers;

    public ContextualDriver(IContextualAnalyzer[] contextualAnalyzers)
    {
        _contextualAnalyzers = contextualAnalyzers;
    }

    public void Drive(ContextualDriverParams driverParams)
    {
        var analyzerParams = new ContextualAnalyzerParams(driverParams.LinkCache, driverParams.LoadOrder);
        foreach (var contextualAnalyzer in _contextualAnalyzers)
        {
            var result = contextualAnalyzer.Analyze(analyzerParams);
            if (result == null) continue;
            foreach (var topic in result.Topics)
            {
                driverParams.ReportDropbox.Dropoff(topic);
            }
        }
    }
}
