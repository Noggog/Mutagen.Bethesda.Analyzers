using Mutagen.Bethesda.Analyzers.Engines;
using Mutagen.Bethesda.Analyzers.SDK.Analyzers;
using Mutagen.Bethesda.Analyzers.SDK.Results;
using Mutagen.Bethesda.Analyzers.SDK.Topics;
using Mutagen.Bethesda.Analyzers.Testing;

namespace Mutagen.Bethesda.Analyzers.Tests;

public static class Utility
{
    public static readonly TopicDefinition Suggestion = new TopicDefinition(
        "A123",
        "TestTitle",
        Severity.Suggestion);
    public static readonly FormattedTopicDefinition FormattedSuggestion = new FormattedTopicDefinition(
        Utility.Suggestion,
        "Test");
    public static readonly IContextualAnalyzer SuggestionAnalyzer;
    public static readonly TopicDefinition Warning = new TopicDefinition(
        "A123",
        "TestTitle",
        Severity.Warning);
    public static readonly FormattedTopicDefinition FormattedWarning = new FormattedTopicDefinition(
        Utility.Warning,
        "Test");
    public static readonly IContextualAnalyzer WarningAnalyzer;

    static Utility()
    {
        SuggestionAnalyzer = CreateAnalyzer(FormattedSuggestion);
        WarningAnalyzer = CreateAnalyzer(FormattedWarning);
    }

    private static IContextualAnalyzer CreateAnalyzer(FormattedTopicDefinition def)
    {
        var analyzerResult = new ContextualAnalyzerResult(new ContextualTopic(def));
        var testAnalyzer = Substitute.For<IContextualAnalyzer>();
        testAnalyzer.Analyze(default(ContextualAnalyzerParams)).ReturnsForAnyArgs(analyzerResult);
        return testAnalyzer;
    }

    public static TestDropoff RunTest(Action<ContainerBuilder> containerAdjustment)
    {
        var builder = new ContainerBuilder();
        builder.RegisterModule<TestModule>();
        containerAdjustment(builder);
        var container = builder.Build();
        var engine = container.Resolve<ContextualEngine>();
        engine.Run();
        return container.Resolve<TestDropoff>();
    }
}
