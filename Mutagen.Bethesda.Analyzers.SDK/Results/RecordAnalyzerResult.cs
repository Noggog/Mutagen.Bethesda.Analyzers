using Mutagen.Bethesda.Analyzers.SDK.Topics;

namespace Mutagen.Bethesda.Analyzers.SDK.Results;

public class RecordAnalyzerResult : IAnalyzerResult<RecordTopic>
{
    private List<RecordTopic> _topics = new();
    public IReadOnlyCollection<RecordTopic> Topics => _topics;

    public RecordAnalyzerResult(RecordTopic topic)
    {
        _topics.Add(topic);
    }

    public RecordAnalyzerResult(params RecordTopic[] topics)
    {
        _topics.AddRange(topics);
    }

    public void AddTopic(RecordTopic topic)
    {
        _topics.Add(topic);
    }
}
