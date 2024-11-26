using Xunit.Sdk;

namespace Europhonium.WebApi.Tests.Acceptance.Utils.Categories;

[TraitDiscoverer(AcceptanceTestDiscoverer.Name, AssemblyName)]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public sealed class AcceptanceTestAttribute : Attribute, ITraitAttribute
{
    private const string AssemblyName = "Europhonium.WebApi.Tests.Acceptance";
}
