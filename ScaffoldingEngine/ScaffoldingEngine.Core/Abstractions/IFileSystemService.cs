namespace ScaffoldingEngine.Core.Abstractions
{
    public interface IFileSystemService
    {
        void CreateDirectory(string path);
        void WriteFile(string path, string content);
    }
}
