using Mutagen.Bethesda.Plugins;

namespace Mutagen.Bethesda.Analyzers.Drivers.RecordFrame;

public interface IRecordFrameAnalyzerBundle : IDriver
{
    RecordType TargetType { get; }
}