using Mutagen.Bethesda.Analyzers.Drivers;
using Mutagen.Bethesda.Analyzers.Reporting;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records.DI;

namespace Mutagen.Bethesda.Analyzers.Engines;

public interface IIsolatedEngine : IEngine
{
    void RunOn(ModPath modPath, IReportDropbox reportDropbox);
}

public class IsolatedEngine : IIsolatedEngine
{
    public IModImporter ModImporter { get; }
    public IDriverProvider<IIsolatedDriver> IsolatedDrivers { get; }

    public IEnumerable<IDriver> Drivers => IsolatedDrivers.Drivers;

    public IsolatedEngine(
        IModImporter modImporter,
        IDriverProvider<IIsolatedDriver> isolatedDrivers)
    {
        ModImporter = modImporter;
        IsolatedDrivers = isolatedDrivers;
    }

    public void RunOn(ModPath modPath, IReportDropbox reportDropbox)
    {
        var mod = ModImporter.Import(modPath);

        var driverParams = new IsolatedDriverParams(
            mod.ToUntypedImmutableLinkCache(),
            reportDropbox,
            mod,
            modPath);

        foreach (var driver in IsolatedDrivers.Drivers)
        {
            driver.Drive(driverParams);
        }
    }
}