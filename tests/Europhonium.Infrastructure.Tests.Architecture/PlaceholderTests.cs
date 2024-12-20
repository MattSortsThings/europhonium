using Europhonium.Infrastructure.Tests.Architecture.Utils;

namespace Europhonium.Infrastructure.Tests.Architecture;

[ArchitectureTest]
public sealed class PlaceholderTests
{
    [Fact]
    public void DoNothing() => true.Should().BeTrue();
}
