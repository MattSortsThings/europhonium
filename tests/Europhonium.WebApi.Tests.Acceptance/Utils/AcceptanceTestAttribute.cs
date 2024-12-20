using Xunit.Sdk;

namespace Europhonium.WebApi.Tests.Acceptance.Utils;

[TraitDiscoverer(AcceptanceTestDiscoverer.Name, "Europhonium.WebApi.Tests.Acceptance")]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public sealed class AcceptanceTestAttribute : Attribute, ITraitAttribute;
