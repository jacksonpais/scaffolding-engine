using ScaffoldingEngine.CLI.Validation;
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
                CliValidator.ValidateFeature(featureName, path);

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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"❌ {ex.Message}");
                Console.ResetColor();
            }

            await Task.CompletedTask;
        }
    }
}
