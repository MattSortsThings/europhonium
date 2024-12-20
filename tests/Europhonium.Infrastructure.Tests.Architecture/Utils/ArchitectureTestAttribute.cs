using Xunit.Sdk;

namespace Europhonium.Infrastructure.Tests.Architecture.Utils;

[TraitDiscoverer(ArchitectureTestDiscoverer.Name, "Europhonium.Infrastructure.Tests.Architecture")]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public sealed class ArchitectureTestAttribute : Attribute, ITraitAttribute;
