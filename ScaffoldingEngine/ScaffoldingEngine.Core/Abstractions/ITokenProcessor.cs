namespace ScaffoldingEngine.Core.Abstractions
{
    public interface ITokenProcessor
    {
        string Process(string content, string featureName);
    }
}
