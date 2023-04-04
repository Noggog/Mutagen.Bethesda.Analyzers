namespace Mutagen.Bethesda.Analyzers.Drivers;

public interface IContextualDriver : IDriver
{
    void Drive(ContextualDriverParams driverParams);
}