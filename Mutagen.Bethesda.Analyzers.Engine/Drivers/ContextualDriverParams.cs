using Mutagen.Bethesda.Analyzers.Reporting;
using Mutagen.Bethesda.Plugins.Cache;
using Mutagen.Bethesda.Plugins.Order;
using Mutagen.Bethesda.Plugins.Records;

namespace Mutagen.Bethesda.Analyzers.Drivers;

public readonly struct ContextualDriverParams
{
    public readonly ILinkCache LinkCache;
    public readonly ILoadOrderGetter<IModListingGetter<IModGetter>> LoadOrder;
    public readonly IReportDropbox ReportDropbox;

    public ContextualDriverParams(
        ILinkCache linkCache,
        ILoadOrderGetter<IModListingGetter<IModGetter>> loadOrder,
        IReportDropbox reportDropbox)
    {
        LinkCache = linkCache;
        LoadOrder = loadOrder;
        ReportDropbox = reportDropbox;
    }
}