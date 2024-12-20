using Xunit.Sdk;

namespace Europhonium.Domain.Tests.Architecture.Utils;

[TraitDiscoverer(ArchitectureTestDiscoverer.Name, "Europhonium.Domain.Tests.Architecture")]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public sealed class ArchitectureTestAttribute : Attribute, ITraitAttribute;
