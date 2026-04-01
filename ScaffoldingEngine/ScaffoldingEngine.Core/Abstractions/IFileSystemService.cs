namespace ScaffoldingEngine.Core.Abstractions
{
    internal interface IFileSystemService
    {
        void CreateDirectory(string path);
        void WriteFile(string path, string content);
    }
}
