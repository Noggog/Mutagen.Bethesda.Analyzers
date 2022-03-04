using System;
using System.IO;
using System.IO.Abstractions;
using Microsoft.Extensions.Logging;
using Mutagen.Bethesda.Analyzers.SDK.Topics;
using Noggog;

namespace Mutagen.Bethesda.Analyzers.Config;

public class AnalyzerConfigReader
{
    public const string SettingEqualString = " = ";

    private readonly IFileSystem _fileSystem;
    private readonly ILogger<AnalyzerConfigReader> _logger;

    public AnalyzerConfigReader(
        IFileSystem fileSystem,
        ILogger<AnalyzerConfigReader> logger)
    {
        _fileSystem = fileSystem;
        _logger = logger;
    }

    public void ReadInto(FilePath path, IAnalyzerConfig config)
    {
        foreach (var line in _fileSystem.File.ReadLines(path))
        {
            var span = line.AsSpan();
            ReadInto(span, config);
        }
    }

    public void ReadInto(ReadOnlySpan<char> line, IAnalyzerConfig config)
    {
        var index = line.IndexOf("#");
        if (index != -1)
        {
            line = line.Slice(0, index);
        }
        if (line.IsWhiteSpace()) return;

        index = line.IndexOf('.');
        if (index == -1)
        {
            _logger.LogError($"Malformed line: {line}");
            return;
        }

        var instruction = line[..index];
        if (!instruction.SequenceEqual("diagnostic")) return;

        line = line.Slice(index + 1);
        index = line.IndexOf('.');
        if (index == -1)
        {
            _logger.LogError($"Malformed line: {line}");
            return;
        }

        TopicId id;
        try
        {
            id = TopicId.Parse(line[..index]);
        }
        catch (ArgumentException e)
        {
            _logger.LogError(e, "Error parsing AnalyzerId");
            return;
        }

        line = line.Slice(index + 1);
        index = line.IndexOf(SettingEqualString);
        if (index == -1)
        {
            _logger.LogError($"Malformed line: {line}");
            return;
        }

        var setting = line[..index];
        if (!setting.SequenceEqual("severity")) return;

        line = line.Slice(index + SettingEqualString.Length);
        if (!Enum.TryParse<Severity>(line, out var sev)) return;

        config.Override(id, sev);
    }
}
