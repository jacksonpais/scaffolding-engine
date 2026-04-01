namespace ScaffoldingEngine.Core.Abstractions
{
    internal interface ITokenProcessor
    {
        string Process(string content, string featureName);
    }
}
