using Mutagen.Bethesda.Plugins.Binary.Headers;
using Mutagen.Bethesda.Plugins.Records;

namespace Mutagen.Bethesda.Analyzers.SDK.Analyzers;

public readonly struct IsolatedRecordFrameAnalyzerParams<TMajor>
    where TMajor : IMajorRecordGetter
{
    public readonly MajorRecordFrame Frame;

    public IsolatedRecordFrameAnalyzerParams(MajorRecordFrame frame)
    {
        Frame = frame;
    }
}

public readonly struct IsolatedRecordFrameAnalyzerParams
{
    public readonly MajorRecordFrame Frame;

    public IsolatedRecordFrameAnalyzerParams(MajorRecordFrame frame)
    {
        Frame = frame;
    }
}