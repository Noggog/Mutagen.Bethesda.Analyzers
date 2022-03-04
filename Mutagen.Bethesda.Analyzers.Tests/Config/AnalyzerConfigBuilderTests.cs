using System.IO;
using System.IO.Abstractions;
using FluentAssertions;
using Mutagen.Bethesda.Analyzers.Config;
using Mutagen.Bethesda.Analyzers.SDK.Topics;
using Xunit;

namespace Mutagen.Bethesda.Analyzers.Tests.Config;

public class AnalyzerConfigBuilderTests
{
    [Theory]
    [AnalyzerAutoData]
    public void LocalOverrideAppliesToConfig(
        IFileSystem fileSystem,
        AnalyzerConfigBuilder sut)
    {
        var localPath = Path.Combine(sut.CurrentDirectoryProvider.CurrentDirectory, AnalyzerConfigBuilder.AnalyzerFileName);
        fileSystem.File.WriteAllLines(
            localPath,
            new[]
            {
                $"diagnostic.{Utility.Suggestion.Id}.severity = Warning",
            });
        var config = sut.Build();
        config.LookupSeverity(Utility.Suggestion).Should().Be(Severity.Warning);
    }
}
