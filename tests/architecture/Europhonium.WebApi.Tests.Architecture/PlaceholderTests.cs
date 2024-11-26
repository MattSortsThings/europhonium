using Europhonium.WebApi.Tests.Architecture.Utils.Categories;

namespace Europhonium.WebApi.Tests.Architecture;

[ArchitectureTest]
public class PlaceholderTests
{
    [Fact]
    public void DoNothing()
    {
        true.Should().BeTrue();
    }
}
