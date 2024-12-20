using Xunit.Abstractions;
using Xunit.Sdk;

namespace Europhonium.WebApi.Tests.Acceptance.Utils;

public sealed class AcceptanceTestDiscoverer : ITraitDiscoverer
{
    internal const string Name = "Europhonium.WebApi.Tests.Acceptance.Utils.AcceptanceTestDiscoverer";

    public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        yield return new KeyValuePair<string, string>("Category", "AcceptanceTest");
    }
}
