using Europhonium.Domain.Tests.Architecture.Utils;

namespace Europhonium.Domain.Tests.Architecture;

[ArchitectureTest]
public sealed class PlaceholderTests
{
    [Fact]
    public void DoNothing() => true.Should().BeTrue();
}
