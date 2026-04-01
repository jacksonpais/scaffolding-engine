using ScaffoldingEngine.Core.Abstractions;
using ScaffoldingEngine.Core.Models;

namespace ScaffoldingEngine.Core.Services
{
    internal class ScaffoldingEngine : IScaffoldingEngine
    {
        private readonly ITemplateProvider _templateProvider;
        private readonly ITokenProcessor _tokenProcessor;
        private readonly IFileSystemService _fileSystem;

        public ScaffoldingEngine(
            ITemplateProvider templateProvider,
            ITokenProcessor tokenProcessor,
            IFileSystemService fileSystem)
        {
            _templateProvider = templateProvider;
            _tokenProcessor = tokenProcessor;
            _fileSystem = fileSystem;
        }

        public void GenerateFeature(FeatureRequest request)
        {
            var templateRoot = _templateProvider.GetTemplateRootPath("Feature");

            var sourceRoot = Directory.GetDirectories(templateRoot).First();
            var targetRoot = Path.Combine(request.TargetPath, request.FeatureName);

            ProcessDirectory(sourceRoot, targetRoot, request.FeatureName);
        }

        private void ProcessDirectory(string sourceDir, string targetDir, string featureName)
        {
            var processedTargetDir = _tokenProcessor.Process(targetDir, featureName);

            _fileSystem.CreateDirectory(processedTargetDir);

            // Process files
            foreach (var file in Directory.GetFiles(sourceDir))
            {
                var fileName = Path.GetFileName(file);
                var processedFileName = _tokenProcessor.Process(fileName, featureName);

                var content = File.ReadAllText(file);
                var processedContent = _tokenProcessor.Process(content, featureName);

                var targetFilePath = Path.Combine(processedTargetDir, processedFileName);

                _fileSystem.WriteFile(targetFilePath, processedContent);
            }

            // Process subdirectories
            foreach (var dir in Directory.GetDirectories(sourceDir))
            {
                var dirName = Path.GetFileName(dir);
                var processedDirName = _tokenProcessor.Process(dirName, featureName);

                var newTargetDir = Path.Combine(processedTargetDir, processedDirName);

                ProcessDirectory(dir, newTargetDir, featureName);
            }
        }
    }
}
