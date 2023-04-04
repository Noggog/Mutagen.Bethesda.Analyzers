using Mutagen.Bethesda.Analyzers.SDK.Analyzers;
using Mutagen.Bethesda.Analyzers.SDK.Topics;
using Mutagen.Bethesda.Analyzers.SDK.Results;
using Mutagen.Bethesda.Skyrim;

namespace Mutagen.Bethesda.Analyzers.Skyrim;

public partial class MissingAssetsAnalyzer : IIsolatedRecordAnalyzer<IHeadPartGetter>
{
    public static readonly TopicDefinition<string> MissingHeadPartModel = MutagenTopicBuilder.FromDiscussion(
            87,
            "Missing Head Part Model file",
            Severity.Error)
        .WithFormatting<string>(MissingModelFileMessageFormat);

    public static readonly TopicDefinition<int, string?> MissingHeadPartFile = MutagenTopicBuilder.FromDiscussion(
            89,
            "Missing Head Part file",
            Severity.CTD)
        .WithFormatting<int, string?>("Missing file for Head Part Part {0} at {1}");

    public RecordAnalyzerResult AnalyzeRecord(IsolatedRecordAnalyzerParams<IHeadPartGetter> param)
    {
        var res = new RecordAnalyzerResult();

        CheckForMissingModelAsset(param.Record, res, MissingHeadPartModel);

        var i = 0;
        foreach (var part in param.Record.Parts)
        {
            CheckForMissingAsset(part.FileName, res, () => RecordTopic.Create(
                param.Record,
                MissingHeadPartFile.Format(i, part.FileName),
                x => x.Parts[0].FileName!));
            i++;
        }

        return res;
    }
}