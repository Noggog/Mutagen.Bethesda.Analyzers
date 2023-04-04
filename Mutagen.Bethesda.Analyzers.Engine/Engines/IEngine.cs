using Mutagen.Bethesda.Analyzers.Drivers;

namespace Mutagen.Bethesda.Analyzers.Engines;

public interface IEngine
{
    IEnumerable<IDriver> Drivers { get; }
}