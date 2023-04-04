using Mutagen.Bethesda.Analyzers.SDK.Topics;

namespace Mutagen.Bethesda.Analyzers;

public static class MutagenTopicBuilder
{
    public const string Prefix = "A";

    public static TopicDefinition FromDiscussion(
        ushort id,
        string title,
        Severity severity)
    {
        return TopicDefinition.FromDiscussion(Prefix, id, title, severity, Constants.AnalyzersDiscussionUrl);
    }
}