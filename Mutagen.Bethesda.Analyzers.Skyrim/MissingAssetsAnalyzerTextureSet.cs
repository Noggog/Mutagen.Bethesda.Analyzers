using Mutagen.Bethesda.Analyzers.SDK.Analyzers;
using Mutagen.Bethesda.Analyzers.SDK.Topics;
using Mutagen.Bethesda.Analyzers.SDK.Results;
using Mutagen.Bethesda.Skyrim;

namespace Mutagen.Bethesda.Analyzers.Skyrim;

public partial class MissingAssetsAnalyzer : IIsolatedRecordAnalyzer<ITextureSetGetter>
{
    public static readonly TopicDefinition<string, string?> MissingTextureInTextureSet = MutagenTopicBuilder.FromDiscussion(
            91,
            "Missing Texture in TextureSet",
            Severity.Error)
        .WithFormatting<string, string?>("Missing texture {0} at {1}");

    private const string TextureSetDiffuseName = nameof(ITextureSet.Diffuse);
    private const string TextureSetNormalOrGlossName = "Normal/Gloss";
    private const string TextureSetEnvironmentMaskOrSubsurfaceTintName = "Environment Maks/Subsurface Tint";
    private const string TextureSetGlowOrDetailMapName = "Glow/Detail Map";
    private const string TextureSetHeightName = nameof(ITextureSet.Height);
    private const string TextureSetEnvironmentName = nameof(ITextureSet.Environment);
    private const string TextureSetMultilayerName = nameof(ITextureSet.Multilayer);
    private const string TextureSetBacklightMaskOrSpecularName = "Backlight Mask/Specular";

    public RecordAnalyzerResult AnalyzeRecord(IsolatedRecordAnalyzerParams<ITextureSetGetter> param)
    {
        var result = new RecordAnalyzerResult();

        CheckForMissingAsset(param.Record.Diffuse, result, () => RecordTopic.Create(param.Record, MissingTextureInTextureSet.Format(TextureSetDiffuseName, param.Record.Diffuse), x => x.Diffuse!));
        CheckForMissingAsset(param.Record.NormalOrGloss, result, () => RecordTopic.Create(param.Record, MissingTextureInTextureSet.Format(TextureSetNormalOrGlossName, param.Record.NormalOrGloss), x => x.NormalOrGloss!));
        CheckForMissingAsset(param.Record.EnvironmentMaskOrSubsurfaceTint, result, () => RecordTopic.Create(param.Record, MissingTextureInTextureSet.Format(TextureSetEnvironmentMaskOrSubsurfaceTintName, param.Record.EnvironmentMaskOrSubsurfaceTint), x => x.EnvironmentMaskOrSubsurfaceTint!));
        CheckForMissingAsset(param.Record.GlowOrDetailMap, result, () => RecordTopic.Create(param.Record, MissingTextureInTextureSet.Format(TextureSetGlowOrDetailMapName, param.Record.GlowOrDetailMap), x => x.GlowOrDetailMap!));
        CheckForMissingAsset(param.Record.Height, result, () => RecordTopic.Create(param.Record, MissingTextureInTextureSet.Format(TextureSetHeightName, param.Record.Height), x => x.Height!));
        CheckForMissingAsset(param.Record.Environment, result, () => RecordTopic.Create(param.Record, MissingTextureInTextureSet.Format(TextureSetEnvironmentName, param.Record.Environment), x => x.Environment!));
        CheckForMissingAsset(param.Record.Multilayer, result, () => RecordTopic.Create(param.Record, MissingTextureInTextureSet.Format(TextureSetMultilayerName, param.Record.Multilayer), x => x.Multilayer!));
        CheckForMissingAsset(param.Record.BacklightMaskOrSpecular, result, () => RecordTopic.Create(param.Record, MissingTextureInTextureSet.Format(TextureSetBacklightMaskOrSpecularName, param.Record.BacklightMaskOrSpecular), x => x.BacklightMaskOrSpecular!));

        return result;
    }
}