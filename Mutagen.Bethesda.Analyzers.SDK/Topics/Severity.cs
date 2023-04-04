namespace Mutagen.Bethesda.Analyzers.SDK.Topics;

public enum Severity : byte
{
    Silent = 0,
    Suggestion = 1,
    Warning = 2,
    Error = 3,
    CTD = 4
}

public static class SeverityExt
{
    public static string ToShortString(this Severity sev)
    {
        return sev switch
        {
            Severity.Suggestion => "SUG",
            Severity.Warning => "WAR",
            Severity.Error => "ERR",
            Severity.CTD => "CTD",
            _ => throw new ArgumentOutOfRangeException(nameof(sev), sev, null)
        };
    }
}
