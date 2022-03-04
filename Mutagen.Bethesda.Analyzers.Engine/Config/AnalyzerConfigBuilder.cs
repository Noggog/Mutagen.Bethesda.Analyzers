using System.IO;
using System.IO.Abstractions;
using Mutagen.Bethesda.Environments.DI;
using Noggog;
using Noggog.IO;

namespace Mutagen.Bethesda.Analyzers.Config;

public class AnalyzerConfigBuilder
{
    public const string AnalyzerFileName = ".analyzerconfig";

    private readonly IFileSystem _fileSystem;
    private readonly IDataDirectoryProvider _dataDirectoryProvider;
    public ICurrentDirectoryProvider CurrentDirectoryProvider { get; }
    private readonly AnalyzerConfigReader _reader;

    public AnalyzerConfigBuilder(
        IFileSystem fileSystem,
        IDataDirectoryProvider dataDirectoryProvider,
        ICurrentDirectoryProvider currentDirectoryProvider,
        AnalyzerConfigReader reader)
    {
        _fileSystem = fileSystem;
        _dataDirectoryProvider = dataDirectoryProvider;
        CurrentDirectoryProvider = currentDirectoryProvider;
        _reader = reader;
    }

    public IAnalyzerConfig Build()
    {
        var config = new AnalyzerConfig();
        LoadIn(Path.Combine(CurrentDirectoryProvider.CurrentDirectory, AnalyzerFileName), config);
        LoadIn(Path.Combine(_dataDirectoryProvider.Path, AnalyzerFileName), config);
        return config;
    }

    private void LoadIn(FilePath path, AnalyzerConfig config)
    {
        if (!path.CheckExists(_fileSystem)) return;
        _reader.ReadInto(path, config);
    }
}
