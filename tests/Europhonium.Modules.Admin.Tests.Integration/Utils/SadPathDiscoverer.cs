using Xunit.Abstractions;
using Xunit.Sdk;

namespace Europhonium.Modules.Admin.Tests.Integration.Utils;

public sealed class SadPathDiscoverer : ITraitDiscoverer
{
    internal const string Name = "Europhonium.Modules.Public.Tests.Integration.Utils.SadPathDiscoverer";

    public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        yield return new KeyValuePair<string, string>("Category", "SadPath");
    }
}
