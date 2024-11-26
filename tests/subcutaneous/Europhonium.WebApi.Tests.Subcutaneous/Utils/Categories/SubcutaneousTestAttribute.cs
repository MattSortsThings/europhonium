using Xunit.Sdk;

namespace Europhonium.WebApi.Tests.Subcutaneous.Utils.Categories;

[TraitDiscoverer(SubcutaneousTestDiscoverer.Name, AssemblyName)]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public sealed class SubcutaneousTestAttribute : Attribute, ITraitAttribute
{
    private const string AssemblyName = "Europhonium.WebApi.Tests.Subcutaneous";
}
