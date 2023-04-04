using Mutagen.Bethesda.Analyzers.Reporting;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Cache;
using Mutagen.Bethesda.Plugins.Records;

namespace Mutagen.Bethesda.Analyzers.Drivers;

public class IsolatedDriverParams
{
    public readonly ILinkCache LinkCache;
    public readonly IReportDropbox ReportDropbox;
    public readonly IModGetter TargetMod;
    public readonly ModPath TargetModPath;

    public IsolatedDriverParams(
        ILinkCache linkCache,
        IReportDropbox reportDropbox,
        IModGetter targetMod,
        ModPath modPath)
    {
        LinkCache = linkCache;
        ReportDropbox = reportDropbox;
        TargetMod = targetMod;
        TargetModPath = modPath;
    }
}