using Mutagen.Bethesda.Analyzers.SDK.Analyzers;
using Mutagen.Bethesda.Analyzers.SDK.Topics;
using Mutagen.Bethesda.Analyzers.SDK.Results;
using Mutagen.Bethesda.Skyrim;

namespace Mutagen.Bethesda.Analyzers.Skyrim;

public partial class MissingAssetsAnalyzer : IIsolatedRecordAnalyzer<IArmorGetter>
{
    public static readonly TopicDefinition<string, string?> MissingArmorModel = MutagenTopicBuilder.FromDiscussion(
            82,
            "Missing Armor Model file",
            Severity.Error)
        .WithFormatting<string, string?>("Missing {0} model file at {1}");

    public RecordAnalyzerResult AnalyzeRecord(IsolatedRecordAnalyzerParams<IArmorGetter> param)
    {
        var result = new RecordAnalyzerResult();

        var femaleFile = param.Record.WorldModel?.Female?.Model?.File;
        CheckForMissingAsset(femaleFile, result, () => RecordTopic.Create(param.Record,
            MissingArmorModel.Format("female", femaleFile),
            x => x.WorldModel!.Female!.Model!.File));

        var maleFile = param.Record.WorldModel?.Male?.Model?.File;
        CheckForMissingAsset(maleFile, result, () => RecordTopic.Create(param.Record,
            MissingArmorModel.Format("male", maleFile),
            x => x.WorldModel!.Male!.Model!.File));

        return result;
    }
}