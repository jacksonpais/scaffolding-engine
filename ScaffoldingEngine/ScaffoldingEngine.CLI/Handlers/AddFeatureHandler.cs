using ScaffoldingEngine.Core.Abstractions;
using ScaffoldingEngine.Core.Models;
using ScaffoldingEngine.Core.Services;

namespace ScaffoldingEngine.CLI.Handlers
{
    public static class AddFeatureHandler
    {
        public static async Task Handle(string featureName, string path)
        {
            try
            {
                Console.WriteLine($"Creating feature: {featureName}");
                Console.WriteLine($"Target path: {path}");

                if (!Directory.Exists(path))
                {
                    Console.WriteLine("❌ Target path does not exist.");
                    return;
                }

                IScaffoldingEngineFeature engine = new ScaffoldingEngineFeature(
                    new TemplateProvider(),
                    new TokenProcessor(),
                    new FileSystemService()
                );

                engine.GenerateFeature(new FeatureRequest
                {
                    FeatureName = featureName,
                    TargetPath = path
                });

                Console.WriteLine("✅ Feature created successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error:");
                Console.WriteLine(ex.Message);
            }

            await Task.CompletedTask;
        }
    }
}
