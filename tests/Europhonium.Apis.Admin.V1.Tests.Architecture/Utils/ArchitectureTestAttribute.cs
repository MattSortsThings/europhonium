using Xunit.Sdk;

namespace Europhonium.Apis.Admin.V1.Tests.Architecture.Utils;

[TraitDiscoverer(ArchitectureTestDiscoverer.Name, "Europhonium.Apis.Admin.V1.Tests.Architecture")]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public sealed class ArchitectureTestAttribute : Attribute, ITraitAttribute;