using Mutagen.Bethesda.Analyzers.SDK.Topics;

namespace Mutagen.Bethesda.Analyzers.SDK.Results;

public class ContextualAnalyzerResult : IAnalyzerResult<ContextualTopic>
{
    private List<ContextualTopic> _topics = new();
    public IReadOnlyCollection<ContextualTopic> Topics => _topics;

    public ContextualAnalyzerResult(ContextualTopic topic)
    {
        _topics.Add(topic);
    }

    public ContextualAnalyzerResult(params ContextualTopic[] topics)
    {
        _topics.AddRange(topics);
    }

    public void AddTopic(ContextualTopic topic)
    {
        _topics.Add(topic);
    }
}
