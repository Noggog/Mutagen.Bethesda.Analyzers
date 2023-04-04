namespace Mutagen.Bethesda.Analyzers.SDK.Analyzers;

public readonly struct IsolatedRecordAnalyzerParams<TMajor>
{
    public readonly TMajor Record;

    public IsolatedRecordAnalyzerParams(TMajor @record)
    {
        Record = record;
    }
}