using Europhonium.Shared.Domain.Abstractions;
using Europhonium.WebApi.Tests.Architecture.Utils;
using Mono.Cecil;
using Mono.Cecil.Rocks;

namespace Europhonium.WebApi.Tests.Architecture;

public static partial class ArchitectureTests
{
    [ArchitectureTest]
    public sealed class DomainTypes
    {
        [Fact]
        public void DomainEntityTypes_ShouldHaveNoPublicConstructors()
        {
            // Arrange
            ConditionList conditions = Types.InAssembly(SharedDomainAssembly)
                .That()
                .Inherit(typeof(Entity<>))
                .Should()
                .MeetCustomRule(new NoPublicConstructorsRule());

            // Act
            TestResult? result = conditions.GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void DomainValueObjectTypes_ShouldImplementIComparable()
        {
            // Arrange
            ConditionList conditions = Types.InAssembly(SharedDomainAssembly)
                .That()
                .Inherit(typeof(ValueObject))
                .Should()
                .ImplementInterface(typeof(IComparable<>));

            // Act
            TestResult? result = conditions.GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        private sealed class NoPublicConstructorsRule : ICustomRule
        {
            public bool MeetsRule(TypeDefinition type) => type.GetConstructors().All(definition => !definition.IsPublic);
        }
    }
}
