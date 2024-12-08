using Europhonium.Shared.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Europhonium.Modules.Admin;

/// <summary>
///     Extension methods to be invoked at application startup.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     Registers all the MediatR services for the Admin Module.
    /// </summary>
    /// <param name="config">Contains MediatR service configuration for the web application.</param>
    /// <returns>
    ///     The same <see cref="MediatRServiceConfiguration" /> instance, so that method invocations can be chained.
    /// </returns>
    public static MediatRServiceConfiguration AddAdminModuleMediatRServices(this MediatRServiceConfiguration config)
    {
        config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

        return config;
    }

    /// <summary>
    ///     Registers the Admin Module's security policy for the web application.
    /// </summary>
    /// <param name="builder">Configures authorization for the web application.</param>
    /// <returns>The same <see cref="AuthorizationBuilder" /> instance, so that method invocations can be chained.</returns>
    public static AuthorizationBuilder AddAdminModuleSecurityPolicy(this AuthorizationBuilder builder)
    {
        builder.AddPolicy(SecurityConstants.Policies.AdminOnly,
            policyBuilder => policyBuilder.RequireRole(SecurityConstants.Roles.Admin));

        return builder;
    }
}
