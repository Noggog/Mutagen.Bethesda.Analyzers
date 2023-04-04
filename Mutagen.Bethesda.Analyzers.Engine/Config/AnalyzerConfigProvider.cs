using Mutagen.Bethesda.Analyzers.SDK.Topics;

namespace Mutagen.Bethesda.Analyzers.Config;

public class AnalyzerConfigProvider : ISeverityLookup
{
    private readonly Lazy<IAnalyzerConfig> _config;
    public IAnalyzerConfig Config => _config.Value;

    public AnalyzerConfigProvider(AnalyzerConfigBuilder builder)
    {
        _config = new Lazy<IAnalyzerConfig>(builder.Build);
    }

    public Severity LookupSeverity(TopicDefinition def)
    {
        return _config.Value.LookupSeverity(def);
    }
}
