using Noggog.StructuredStrings;

namespace Mutagen.Bethesda.Analyzers.SDK.Topics;

public static class TopicPrinting
{
    public static string ToShortString(this TopicDefinition topic)
    {
        return $"[{topic.Id}][{topic.Severity.ToShortString()}] {topic.Title}";
    }

    public static void Append(this TopicDefinition topic, StructuredStringBuilder sb)
    {
        sb.AppendLine(topic.ToShortString());
        using (sb.IncreaseDepth())
        {
            if (topic.InformationUri != null)
            {
                sb.AppendLine(topic.InformationUri.ToString());
            }
        }
    }
}
