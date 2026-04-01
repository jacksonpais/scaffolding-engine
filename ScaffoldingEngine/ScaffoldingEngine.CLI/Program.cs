using ScaffoldingEngine.CLI.Commands;
using System.CommandLine;

namespace ScaffoldingEngine.CLI;

public class Program
{
    static async Task<int> Main(string[] args)
    {
        var rootCommand = new RootCommand
        {
            Description = "Scaffolding CLI Tool"
        };

        rootCommand.Subcommands.Add(AddFeatureCommand.Build());

        var result = await rootCommand.Parse(args).InvokeAsync();

        // ✅ Only pause during debugging
        if (System.Diagnostics.Debugger.IsAttached)
        {
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        return result;
    }
}