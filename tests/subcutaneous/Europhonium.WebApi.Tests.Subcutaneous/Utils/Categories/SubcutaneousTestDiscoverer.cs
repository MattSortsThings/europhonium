using Xunit.Abstractions;
using Xunit.Sdk;

namespace Europhonium.WebApi.Tests.Subcutaneous.Utils.Categories;

public sealed class SubcutaneousTestDiscoverer : ITraitDiscoverer
{
    internal const string Name = "Europhonium.WebApi.Tests.Subcutaneous.Utils.Categories.SubcutaneousTestDiscoverer";

    public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        yield return new KeyValuePair<string, string>("Category", "SubcutaneousTest");
    }
}
