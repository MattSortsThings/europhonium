using Europhonium.WebApi.Tests.Architecture.Utils;
using Mono.Cecil;

namespace Europhonium.WebApi.Tests.Architecture;

public static partial class ArchitectureTests
{
    [ArchitectureTest]
    public sealed class FeatureFiles
    {
        private static PredicateList FeatureFileTypes() =>
            Types.InAssemblies([AdminModuleAssembly, PublicModuleAssembly])
                .That()
                .ArePublic()
                .And()
                .AreStatic()
                .And()
                .AreNotNested()
                .And()
                .DoNotResideInNamespaceMatching(@"^Europhonium\.Modules\.[A-Za-z]+$");

        [Fact]
        public void FeatureFile_ShouldHaveNameBeginningWithImperativeVerb()
        {
            // Arrange
            ConditionList conditions = FeatureFileTypes()
                .Should()
                .HaveNameMatching("^Get|Create|Delete");

            // Act
            TestResult result = conditions.GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void FeatureFile_ShouldHaveSinglePublicMethod_WhichShouldBeStaticAndHaveNameExecuteAsync()
        {
            // Arrange
            ConditionList conditions = FeatureFileTypes()
                .Should()
                .MeetCustomRule(new SolePublicMethodIsStaticAndHasNameExecuteAsyncRule());

            // Act
            TestResult result = conditions.GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void FeatureFile_ShouldHaveNoNestedPublicTypes_ThatHaveNamesOtherThanRequestOrResponse()
        {
            // Arrange
            ConditionList conditions = FeatureFileTypes()
                .Should()
                .MeetCustomRule(new NoNestedPublicTypesWithNamesOtherThanRequestOrResponseRule());

            // Act
            TestResult result = conditions.GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void FeatureFile_ShouldHaveOneNestedTypeThatImplementsIRequest_WhichShouldBeNamedCommandOrQuery()
        {
            // Arrange
            ConditionList conditions = FeatureFileTypes()
                .Should()
                .MeetCustomRule(new SoleNestedTypeThatImplementsIRequestHasNameCommandOrQueryRule());

            // Act
            TestResult result = conditions.GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void FeatureFile_ShouldHaveOneNestedTypeThatImplementsIRequestHandler_WhichShouldBeNamedHandler()
        {
            // Arrange
            ConditionList conditions = FeatureFileTypes()
                .Should()
                .MeetCustomRule(new SoleNestedTypeThatImplementsIRequestHandlerHasNameHandlerRule());

            // Act
            TestResult result = conditions.GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        private sealed class SolePublicMethodIsStaticAndHasNameExecuteAsyncRule : ICustomRule
        {
            public bool MeetsRule(TypeDefinition type)
            {
                return type.Methods.Single(definition =>
                        definition.IsPublic)
                    is { IsStatic: true, Name: "ExecuteAsync" };
            }
        }

        private sealed class NoNestedPublicTypesWithNamesOtherThanRequestOrResponseRule : ICustomRule
        {
            public bool MeetsRule(TypeDefinition type)
            {
                return type.NestedTypes.Where(nestedType =>
                        nestedType.IsPublic)
                    .All(nestedType => nestedType.Name is "Request" or "Response");
            }
        }

        private sealed class SoleNestedTypeThatImplementsIRequestHasNameCommandOrQueryRule : ICustomRule
        {
            public bool MeetsRule(TypeDefinition type)
            {
                return type.NestedTypes.Single(nestedTypeDefinition =>
                        nestedTypeDefinition.Interfaces.Any(implementation =>
                            implementation.InterfaceType.FullName.StartsWith("MediatR.IRequest`1")))
                    is { Name: "Command" or "Query" };
            }
        }

        private sealed class SoleNestedTypeThatImplementsIRequestHandlerHasNameHandlerRule : ICustomRule
        {
            public bool MeetsRule(TypeDefinition type)
            {
                return type.NestedTypes.Single(nestedTypeDefinition =>
                        nestedTypeDefinition.Interfaces.Any(implementation =>
                            implementation.InterfaceType.FullName.StartsWith("MediatR.IRequestHandler`2")))
                    is { Name: "Handler" };
            }
        }
    }
}
