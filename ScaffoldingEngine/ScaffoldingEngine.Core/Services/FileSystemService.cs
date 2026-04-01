using ScaffoldingEngine.Core.Abstractions;

namespace ScaffoldingEngine.Core.Services
{
    public class FileSystemService : IFileSystemService
    {
        public void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public void WriteFile(string path, string content)
        {
            File.WriteAllText(path, content);
        }
    }
}
