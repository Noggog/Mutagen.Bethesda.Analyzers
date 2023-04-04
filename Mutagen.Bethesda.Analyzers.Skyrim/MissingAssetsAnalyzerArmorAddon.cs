using Mutagen.Bethesda.Analyzers.SDK.Analyzers;
using Mutagen.Bethesda.Analyzers.SDK.Topics;
using Mutagen.Bethesda.Analyzers.SDK.Results;
using Mutagen.Bethesda.Skyrim;

namespace Mutagen.Bethesda.Analyzers.Skyrim;

public partial class MissingAssetsAnalyzer : IIsolatedRecordAnalyzer<IArmorAddonGetter>
{
    public static readonly TopicDefinition<string, string?> MissingArmorAddonWorldModel = MutagenTopicBuilder.FromDiscussion(
            84,
            "Missing Armor Addon Model file",
            Severity.Error)
        .WithFormatting<string, string?>("Missing {0} Armor Addon Model file at {1}");

    public static readonly TopicDefinition<string, string?> MissingArmorAddonFirstPersonModel = MutagenTopicBuilder.FromDiscussion(
            85,
            "Missing Armor Addon 1st Person Model file",
            Severity.Error)
        .WithFormatting<string, string?>("Missing {0} 1st Person Armor Addon Model file at {1}");

    public RecordAnalyzerResult AnalyzeRecord(IsolatedRecordAnalyzerParams<IArmorAddonGetter> param)
    {
        var res = new RecordAnalyzerResult();

        var femaleWorldModel = param.Record.WorldModel?.Female?.File;
        CheckForMissingAsset(femaleWorldModel, res, () => RecordTopic.Create(
            param.Record,
            MissingArmorAddonWorldModel.Format("female", femaleWorldModel),
            x => x.WorldModel!.Female!.File));

        var maleWorldModel = param.Record.WorldModel?.Male?.File;
        CheckForMissingAsset(maleWorldModel, res, () => RecordTopic.Create(
            param.Record,
            MissingArmorAddonWorldModel.Format("male", maleWorldModel),
            x => x.WorldModel!.Male!.File));

        var femaleFirstPersonModel = param.Record.FirstPersonModel?.Female?.File;
        CheckForMissingAsset(femaleFirstPersonModel, res, () => RecordTopic.Create(
            param.Record,
            MissingArmorAddonFirstPersonModel.Format("female", femaleFirstPersonModel),
            x => x.FirstPersonModel!.Female!.File));

        var maleFirstPersonModel = param.Record.FirstPersonModel?.Male?.File;
        CheckForMissingAsset(maleFirstPersonModel, res, () => RecordTopic.Create(
            param.Record,
            MissingArmorAddonFirstPersonModel.Format("male", maleFirstPersonModel),
            x => x.FirstPersonModel!.Male!.File));

        return res;
    }
}