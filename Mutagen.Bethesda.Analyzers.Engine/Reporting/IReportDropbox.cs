using Mutagen.Bethesda.Analyzers.SDK.Topics;
using Mutagen.Bethesda.Plugins.Records;

namespace Mutagen.Bethesda.Analyzers.Reporting;

public interface IReportDropbox
{
    void Dropoff(
        IModGetter sourceMod,
        IMajorRecordGetter majorRecord,
        ITopic topic);

    void Dropoff(ITopic topic);
}