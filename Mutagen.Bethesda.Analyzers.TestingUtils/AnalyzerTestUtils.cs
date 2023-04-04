using Mutagen.Bethesda.Analyzers.SDK.Topics;
using Mutagen.Bethesda.Analyzers.SDK.Results;
using Xunit;

namespace Mutagen.Bethesda.Analyzers.TestingUtils;

public static class AnalyzerTestUtils
{
    public static void HasTopic<TError>(IAnalyzerResult<TError> result, TopicDefinition topicDefinition, int count = -1)
        where TError : ITopic
    {
        if (count == -1)
        {
            Assert.True(result.Topics.Any(x => x.TopicDefinition.Equals(topicDefinition)));
        }
        else
        {
            var errors = result.Topics
                .Where(x => x.TopicDefinition.Equals(topicDefinition))
                .ToList();

            Assert.Equal(count, errors.Count);
        }
    }
}
