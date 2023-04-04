using Mutagen.Bethesda.Analyzers.Reporting;
using Mutagen.Bethesda.Analyzers.SDK.Topics;
using Mutagen.Bethesda.Plugins.Records;

namespace Mutagen.Bethesda.Analyzers.Testing;

public class TestDropoff : IReportDropbox
{
    public int TotalReports => Reports.Count;
    private readonly List<ITopic> _reports = new();
    public IReadOnlyList<ITopic> Reports => _reports;

    public void Dropoff(IModGetter sourceMod, IMajorRecordGetter majorRecord, ITopic topic)
    {
        _reports.Add(topic);
    }

    public void Dropoff(ITopic topic)
    {
        _reports.Add(topic);
    }
}
