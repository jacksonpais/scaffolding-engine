using ScaffoldingEngine.Core.Abstractions;
using ScaffoldingEngine.Core.Models;
using ScaffoldingEngine.Core.Services;

namespace ScaffoldingEngine.CLI;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Scaffolding CLI Started ===");
        try
        {
            // Debug: print all args
            Console.WriteLine($"Args count: {args.Length}");
            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine($"Arg[{i}]: {args[i]}");
            }

            // Validate input
            if (args.Length < 2)
            {
                Console.WriteLine("\nInvalid arguments.");
                Console.WriteLine("Usage:");
                Console.WriteLine("dotnet run -- <FeatureName> <TargetPath>");
                return;
            }

            var featureName = args[0];
            var targetPath = args[1];

            Console.WriteLine($"\nCreating feature: {featureName}");
            Console.WriteLine($"Target path: {targetPath}");

            // Validate path
            if (!Directory.Exists(targetPath))
            {
                Console.WriteLine("Target path does not exist.");
                return;
            }

            // Setup dependencies
            IScaffoldingEngineFeature engine = new ScaffoldingEngineFeature(
                new TemplateProvider(),
                new TokenProcessor(),
                new FileSystemService()
            );

            // Execute
            engine.GenerateFeature(new FeatureRequest
            {
                FeatureName = featureName,
                TargetPath = targetPath
            });

            Console.WriteLine("\nFeature created successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("\nError occurred:");
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
        }
        finally
        {
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}