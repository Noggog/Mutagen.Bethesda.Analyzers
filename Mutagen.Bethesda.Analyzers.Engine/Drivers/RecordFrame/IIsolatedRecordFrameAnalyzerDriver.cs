using Mutagen.Bethesda.Plugins.Binary.Headers;

namespace Mutagen.Bethesda.Analyzers.Drivers.RecordFrame;

public interface IIsolatedRecordFrameAnalyzerDriver : IRecordFrameAnalyzerBundle
{
    void Drive(IsolatedDriverParams driverParams, MajorRecordFrame frame);
}