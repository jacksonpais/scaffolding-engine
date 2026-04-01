using ScaffoldingEngine.Core.Abstractions;
using ScaffoldingEngine.Core.Models;
using ScaffoldingEngine.Core.Services;

namespace ScaffoldingEngine.CLI;

public class Program
{
    static void Main(string[] args)
    {
        try
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage:");
                Console.WriteLine("mytool <FeatureName> <TargetPath>");
                return;
            }

            var featureName = args[0];
            var targetPath = args[1];

            Console.WriteLine($"Creating feature: {featureName}");
            Console.WriteLine($"Target path: {targetPath}");

            // Setup dependencies manually (simple for now)
            IScaffoldingEngineFeature engine = new ScaffoldingEngineFeature(
                new TemplateProvider(),
                new TokenProcessor(),
                new FileSystemService()
            );

            engine.GenerateFeature(new FeatureRequest
            {
                FeatureName = featureName,
                TargetPath = targetPath
            });

            Console.WriteLine("✅ Feature created successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ Error:");
            Console.WriteLine(ex.Message);
        }
    }
}