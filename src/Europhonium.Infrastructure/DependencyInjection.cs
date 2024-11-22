using Microsoft.Extensions.DependencyInjection;

namespace Europhonium.Infrastructure;

public static class DependencyInjection
{
    /// <summary>
    ///     Registers all the infrastructure-tier services for the web application.
    /// </summary>
    /// <param name="services">Contains service descriptors for the web application.</param>
    /// <returns>The same <see cref="IServiceCollection" /> instance, so that method invocations can be chained.</returns>
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services) => services;
}
