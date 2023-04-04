using CommandLine;
using Mutagen.Bethesda.Analyzers.Config;
using Mutagen.Bethesda.Analyzers.SDK.Topics;

namespace Mutagen.Bethesda.Analyzers.Cli.Args;

[Verb("run-analyzers", HelpText = "Run analyzers on a game installation")]
public class RunAnalyzersCommand : IMinimumSeverityConfiguration
{
    [Option('g', "GameRelease", Required = true, HelpText = "Game Release to target")]
    public GameRelease GameRelease { get; set; }

    [Option("PrintTopics", Required = false, HelpText = "Whether to print the topics being run")]
    public bool PrintTopics { get; set; } = false;

    [Option('s', "Severity", HelpText = "Minimum severity required in order to report")]

    public Severity MinimumSeverity { get; set; } = Severity.Suggestion;
}