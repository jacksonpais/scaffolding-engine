namespace ScaffoldingEngine.Core.Abstractions
{
    internal interface ITemplateProvider
    {
        string GetTemplateRootPath(string templateName);
    }
}
