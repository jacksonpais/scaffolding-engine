using ScaffoldingEngine.Core.Models;

namespace ScaffoldingEngine.Core.Abstractions
{
    public interface IScaffoldingEngineFeature
    {
        void GenerateFeature(FeatureRequest request);
    }
}
