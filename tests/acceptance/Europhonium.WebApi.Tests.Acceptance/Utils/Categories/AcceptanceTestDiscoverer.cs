using Xunit.Abstractions;
using Xunit.Sdk;

namespace Europhonium.WebApi.Tests.Acceptance.Utils.Categories;

public sealed class AcceptanceTestDiscoverer : ITraitDiscoverer
{
    internal const string Name = "Europhonium.WebApi.Tests.Acceptance.Utils.Categories.AcceptanceTestDiscoverer";

    public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        yield return new KeyValuePair<string, string>("Category", "AcceptanceTest");
    }
}
