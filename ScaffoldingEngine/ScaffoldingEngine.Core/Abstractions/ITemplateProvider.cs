namespace ScaffoldingEngine.Core.Abstractions
{
    public interface ITemplateProvider
    {
        string GetTemplateRootPath(string templateName);
    }
}
