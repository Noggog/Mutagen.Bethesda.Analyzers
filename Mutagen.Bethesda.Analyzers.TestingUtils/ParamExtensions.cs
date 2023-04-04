using Mutagen.Bethesda.Analyzers.SDK.Analyzers;
using Mutagen.Bethesda.Plugins.Records;

namespace Mutagen.Bethesda.Analyzers.TestingUtils;

public static class ParamExtensions
{
    public static IsolatedRecordAnalyzerParams<TMajor> AsIsolatedParams<TMajor>(this TMajor maj)
        where TMajor : IMajorRecordGetter
    {
        return new IsolatedRecordAnalyzerParams<TMajor>(maj);
    }
}