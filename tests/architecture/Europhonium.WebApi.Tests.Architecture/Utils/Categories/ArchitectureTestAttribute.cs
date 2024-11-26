using Xunit.Sdk;

namespace Europhonium.WebApi.Tests.Architecture.Utils.Categories;

[TraitDiscoverer(ArchitectureTestDiscoverer.Name, AssemblyName)]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public sealed class ArchitectureTestAttribute : Attribute, ITraitAttribute
{
    private const string AssemblyName = "Europhonium.WebApi.Tests.Architecture";
}
