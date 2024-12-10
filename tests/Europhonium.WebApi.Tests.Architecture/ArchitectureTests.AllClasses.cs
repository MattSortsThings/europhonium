using System.Reflection;
using Europhonium.Modules.Admin.Countries;
using Europhonium.Modules.Public.Queryables;
using Europhonium.Shared.Domain.Abstractions;
using Europhonium.Shared.Infrastructure.DataAccess;
using Europhonium.WebApi.Tests.Architecture.Utils;

namespace Europhonium.WebApi.Tests.Architecture;

public static partial class ArchitectureTests
{
    private static readonly Assembly AdminModuleAssembly = typeof(CreateCountry).Assembly;
    private static readonly Assembly PublicModuleAssembly = typeof(QueryableCountryResource).Assembly;
    private static readonly Assembly SharedDomainAssembly = typeof(ValueObject).Assembly;
    private static readonly Assembly SharedInfrastructureAssembly = typeof(DataAccessConstants).Assembly;
    private static readonly Assembly WebApiAssembly = typeof(IWebApiAssemblyLocator).Assembly;

    [ArchitectureTest]
    public sealed class AllClasses
    {
        [Fact]
        public void NonAbstractClassesInAllAssemblies_ShouldBeSealed()
        {
            // Arrange
            ConditionList conditions = Types.InAssemblies([
                    AdminModuleAssembly,
                    PublicModuleAssembly,
                    SharedDomainAssembly,
                    SharedInfrastructureAssembly,
                    WebApiAssembly
                ])
                .That()
                .AreClasses()
                .And()
                .AreNotAbstract()
                .And()
                .DoNotResideInNamespaceContaining("Migrations")
                .And()
                .DoNotHaveName("Program")
                .Should()
                .BeSealed();

            // Act
            TestResult result = conditions.GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }
    }
}
