using Microsoft.Extensions.DependencyInjection;

namespace Europhonium.Infrastructure;

/// <summary>
///     Extension methods to be invoked at application startup.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     Adds all the infrastructure services for the web application.
    /// </summary>
    /// <param name="services">Contains service descriptors for the web application.</param>
    /// <returns>The same <see cref="IServiceCollection" /> instance, so that method invocations can be chained.</returns>
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services) => services;
}
