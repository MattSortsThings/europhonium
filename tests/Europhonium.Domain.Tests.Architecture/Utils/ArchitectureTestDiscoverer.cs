using Xunit.Abstractions;
using Xunit.Sdk;

namespace Europhonium.Domain.Tests.Architecture.Utils;

public sealed class ArchitectureTestDiscoverer : ITraitDiscoverer
{
    internal const string Name = "Europhonium.Domain.Tests.Architecture.Utils.ArchitectureTestDiscoverer";

    public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        yield return new KeyValuePair<string, string>("Category", "ArchitectureTest");
    }
}