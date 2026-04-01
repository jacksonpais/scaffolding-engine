using ScaffoldingEngine.Core.Abstractions;

public class TokenProcessor : ITokenProcessor
{
    public string Process(string content, string featureName)
    {
        return content
            .Replace("__FeatureName__", featureName)
            .Replace("__featureName__", ToCamelCase(featureName))
            .Replace("__FEATURE_NAME__", featureName.ToUpper());
    }

    private string ToCamelCase(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;
        return char.ToLower(input[0]) + input.Substring(1);
    }
}