using Mutagen.Bethesda.Plugins.Binary.Headers;

namespace Mutagen.Bethesda.Analyzers.Drivers.RecordFrame;

public interface IContextualRecordFrameAnalyzerDriver : IRecordFrameAnalyzerBundle
{
    void Drive(ContextualDriverParams driverParams, MajorRecordFrame frame);
}