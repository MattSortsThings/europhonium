using Xunit.Sdk;

namespace Europhonium.WebApi.Tests.Architecture.Utils;

[TraitDiscoverer(ArchitectureTestDiscoverer.Name, "Europhonium.WebApi.Tests.Architecture")]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public sealed class ArchitectureTestAttribute : Attribute, ITraitAttribute;
