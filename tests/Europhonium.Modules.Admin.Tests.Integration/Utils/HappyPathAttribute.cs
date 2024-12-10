using Xunit.Sdk;

namespace Europhonium.Modules.Admin.Tests.Integration.Utils;

[TraitDiscoverer(HappyPathDiscoverer.Name, "Europhonium.Modules.Public.Tests.Integration")]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public sealed class HappyPathAttribute : Attribute, ITraitAttribute;
