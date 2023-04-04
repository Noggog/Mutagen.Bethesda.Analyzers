using Mutagen.Bethesda.Analyzers.SDK.Topics;

namespace Mutagen.Bethesda.Analyzers.SDK.Results;

public class RecordFrameAnalyzerResult : IAnalyzerResult<RecordFrameTopic>
{
    private List<RecordFrameTopic> _topics = new();
    public IReadOnlyCollection<RecordFrameTopic> Topics => _topics;

    public RecordFrameAnalyzerResult(RecordFrameTopic topic)
    {
        _topics.Add(topic);
    }

    public RecordFrameAnalyzerResult(params RecordFrameTopic[] topics)
    {
        _topics.AddRange(topics);
    }

    public void AddTopic(RecordFrameTopic topic)
    {
        _topics.Add(topic);
    }
}