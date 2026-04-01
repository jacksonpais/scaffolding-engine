using ScaffoldingEngine.Core.Abstractions;

namespace ScaffoldingEngine.Core.Services
{
    public class TemplateProvider : ITemplateProvider
    {
        public string GetTemplateRootPath(string templateName)
        {
            var basePath = AppContext.BaseDirectory;

            return Path.Combine(basePath, "Templates", templateName);
        }
    }
}
