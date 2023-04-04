using Mutagen.Bethesda.Analyzers.SDK.Analyzers;

namespace Mutagen.Bethesda.Analyzers.Drivers;

public interface IDriver
{
    bool Applicable { get; }
    public IEnumerable<IAnalyzer> Analyzers { get; }
}