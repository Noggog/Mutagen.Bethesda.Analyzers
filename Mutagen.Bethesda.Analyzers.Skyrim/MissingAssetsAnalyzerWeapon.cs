using Mutagen.Bethesda.Analyzers.SDK.Analyzers;
using Mutagen.Bethesda.Analyzers.SDK.Topics;
using Mutagen.Bethesda.Analyzers.SDK.Results;
using Mutagen.Bethesda.Skyrim;

namespace Mutagen.Bethesda.Analyzers.Skyrim;

public partial class MissingAssetsAnalyzer : IIsolatedRecordAnalyzer<IWeaponGetter>
{
    public static readonly TopicDefinition<string> MissingWeaponModel = MutagenTopicBuilder.FromDiscussion(
            92,
            "Missing Weapon Model file",
            Severity.Error)
        .WithFormatting<string>(MissingModelFileMessageFormat);

    public RecordAnalyzerResult AnalyzeRecord(IsolatedRecordAnalyzerParams<IWeaponGetter> param)
    {
        var res = new RecordAnalyzerResult();
        CheckForMissingModelAsset(param.Record, res, MissingWeaponModel);
        return res;
    }
}