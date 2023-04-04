using Mutagen.Bethesda.Analyzers.SDK.Topics;

namespace Mutagen.Bethesda.Analyzers.SDK.Results;

public interface IAnalyzerResult<TError>
    where TError : ITopic
{
    IReadOnlyCollection<TError> Topics { get; }
}
