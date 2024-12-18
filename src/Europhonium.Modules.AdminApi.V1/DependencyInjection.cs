using Microsoft.Extensions.DependencyInjection;

namespace Europhonium.Modules.AdminApi.V1;

/// <summary>
///     Extension methods to be invoked at application startup.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     Adds configuration for the Admin API V1 Module to the MediatR service configuration for the web application.
    /// </summary>
    /// <param name="config">Contains MediatR service configuration for the web application.</param>
    /// <returns>
    ///     The same <see cref="MediatRServiceConfiguration" /> instance, so that method invocations can be chained.
    /// </returns>
    public static MediatRServiceConfiguration AddAdminApiV1Configuration(this MediatRServiceConfiguration config)
    {
        config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

        return config;
    }
}
