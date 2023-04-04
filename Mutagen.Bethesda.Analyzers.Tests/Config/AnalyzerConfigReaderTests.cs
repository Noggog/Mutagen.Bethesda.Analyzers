using Mutagen.Bethesda.Analyzers.Config;
using Mutagen.Bethesda.Analyzers.SDK.Topics;

namespace Mutagen.Bethesda.Analyzers.Tests.Config;

public class AnalyzerConfigReaderTests
{
    [Theory]
    [AnalyzerInlineData("diagnostic.A123.severity = Warning")]
    [AnalyzerInlineData("diagnostic.A123.severity = Warning # A Comment")]
    public void LinesThatParse(
        string line,
        IAnalyzerConfig config,
        AnalyzerConfigReader sut)
    {
        sut.ReadInto(line.AsSpan(), config);
        config.Received(1).Override(new TopicId("A", 123), Severity.Warning);
    }

    [Theory]
    [AnalyzerInlineData("diagnostic.A123.severity = Warning And Gibberish")]
    [AnalyzerInlineData("other.A123.severity = Warning")]
    [AnalyzerInlineData("diagnostic.1A123.severity = Warning")]
    [AnalyzerInlineData("diagnostic.A123")]
    [AnalyzerInlineData("diagnostic.A123.other = Warning")]
    [AnalyzerInlineData("diagnostic.A123.severity = Other")]
    [AnalyzerInlineData("diagnostic.A123.severity")]
    [AnalyzerInlineData("")]
    [AnalyzerInlineData("#diagnostic.A123.severity = Warning")]
    public void AbnormalLinesDontOverride(
        string line,
        IAnalyzerConfig config,
        AnalyzerConfigReader sut)
    {
        sut.ReadInto(line.AsSpan(), config);
        config.DidNotReceiveWithAnyArgs().Override(default!, default);
    }
}
