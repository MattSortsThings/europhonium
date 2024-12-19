using Microsoft.Extensions.DependencyInjection;

namespace Europhonium.Apis.Admin.V1;

/// <summary>
///     Extension methods to be invoked at application startup.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     Adds the <see cref="MediatR" /> service configuration for Version 1 of the Admin API.
    /// </summary>
    /// <param name="config">Contains <see cref="MediatR" /> service configuration settings for the web application.</param>
    /// <returns>
    ///     The same <see cref="MediatRServiceConfiguration" /> instance, so that method invocations can be chained.
    /// </returns>
    public static MediatRServiceConfiguration AddAdminApiV1Configuration(this MediatRServiceConfiguration config)
    {
        config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

        return config;
    }
}
