using ScaffoldingEngine.Core.Models;

namespace ScaffoldingEngine.Core.Abstractions
{
    internal interface IScaffoldingEngine
    {
        void GenerateFeature(FeatureRequest request);
    }
}
