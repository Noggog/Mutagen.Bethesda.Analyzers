using System.Linq.Expressions;

namespace Mutagen.Bethesda.Analyzers.SDK.Topics;

public record RecordTopic(IFormattedTopicDefinition _formattedTopicDefinition, Expression MemberExpression) : ITopic
{
    private readonly IFormattedTopicDefinition _formattedTopicDefinition = _formattedTopicDefinition;

    public static RecordTopic Create<T>(T obj, IFormattedTopicDefinition formattedTopicDefinition, Expression<Func<T, object>> memberExpression)
    {
        return new RecordTopic(formattedTopicDefinition, memberExpression);
    }

    public TopicDefinition TopicDefinition => _formattedTopicDefinition.TopicDefinition;
    public string FormattedMessage => _formattedTopicDefinition.FormattedMessage;
    public Severity Severity { get; set; } = _formattedTopicDefinition.TopicDefinition.Severity;
}
