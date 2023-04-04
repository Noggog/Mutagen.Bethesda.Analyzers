using CommandLine;
using Mutagen.Bethesda.Analyzers.Cli.Args;

namespace Mutagen.Bethesda.Analyzers.Cli;

class Program
{
    static async Task<int> Main(string[] args)
    {
        try
        {
            return await Parser.Default.ParseArguments(args, typeof(RunAnalyzersCommand))
                .MapResult(
                    (RunAnalyzersCommand cmd) => RunAnalyzers.Run(cmd),
                    async _ =>
                    {
                        return -1;
                    }).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            System.Console.Error.WriteLine(ex);
            return -1;
        }
    }
}