using Mutagen.Bethesda.Analyzers.SDK.Results;
using Mutagen.Bethesda.Plugins.Records;

namespace Mutagen.Bethesda.Analyzers.SDK.Analyzers;

public interface IIsolatedRecordFrameAnalyzer<TMajor> : IAnalyzer
    where TMajor : IMajorRecordGetter
{
    RecordFrameAnalyzerResult? AnalyzeRecord(IsolatedRecordFrameAnalyzerParams<TMajor> param);
}

public interface IIsolatedRecordFrameAnalyzer : IAnalyzer
{
    RecordFrameAnalyzerResult? AnalyzeRecord(IsolatedRecordFrameAnalyzerParams param);
}