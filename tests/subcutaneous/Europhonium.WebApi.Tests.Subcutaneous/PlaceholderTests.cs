using Europhonium.WebApi.Tests.Subcutaneous.Utils.Categories;

namespace Europhonium.WebApi.Tests.Subcutaneous;

[SubcutaneousTest]
public class PlaceholderTests
{
    [Fact]
    public void DoNothing()
    {
        true.Should().BeTrue();
    }
}
