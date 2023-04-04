using Mutagen.Bethesda.Analyzers.SDK.Topics;

namespace Mutagen.Bethesda.Analyzers.SDK.Analyzers;

public interface IAnalyzer
{
    IEnumerable<TopicDefinition> Topics { get; }
}
