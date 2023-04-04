using System.IO.Abstractions;
using Microsoft.Extensions.Logging;
using Mutagen.Bethesda.Analyzers.SDK.Topics;
using Mutagen.Bethesda.Analyzers.SDK.Results;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Skyrim;

namespace Mutagen.Bethesda.Analyzers.Skyrim;

public partial class MissingAssetsAnalyzer
{
    public string Author => "erri120";
    public string Description => "Finds missing assets.";

    private const string MissingModelFileMessageFormat = "Missing Model file {0}";

    private readonly ILogger<MissingAssetsAnalyzer> _logger;
    private readonly IFileSystem _fileSystem;

    public MissingAssetsAnalyzer(ILogger<MissingAssetsAnalyzer> logger, IFileSystem fileSystem)
    {
        _logger = logger;
        _fileSystem = fileSystem;
    }

    private void CheckForMissingModelAsset<TMajorRecordGetter>(
        TMajorRecordGetter modeledGetter,
        RecordAnalyzerResult result,
        TopicDefinition<string> topicDefinition)
        where TMajorRecordGetter : IMajorRecordGetter, IModeledGetter
    {
        var path = modeledGetter.Model?.File;
        if (path == null) return;
        if (FileExists(path)) return;

        var error = RecordTopic.Create(
            modeledGetter,
            topicDefinition.Format(path),
            x => x.Model!.File);

        result.AddTopic(error);
    }

    private void CheckForMissingAsset(string? path, RecordAnalyzerResult result, Func<RecordTopic> func)
    {
        if (path == null) return;
        if (FileExists(path)) return;

        var error = func();
        result.AddTopic(error);
    }

    private bool FileExists(string path) => _fileSystem.File.Exists(path);
}