using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Europhonium.Endpoints.Shared.Security;

internal static class DependencyInjection
{
    /// <summary>
    ///     Registers the API key authentication and authentication services for the endpoints.
    /// </summary>
    /// <param name="services">Contains service descriptors for the application.</param>
    /// <param name="configuration">Contains configuration properties for the web application.</param>
    /// <returns>The same <see cref="IServiceCollection" /> instance, so that method invocations can be chained.</returns>
    internal static IServiceCollection AddSecurityServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApiKeyOptions>(configuration.GetSection(nameof(ApiKeyOptions)));

        services.AddAuthentication(nameof(ApiKeyAuthenticationScheme))
            .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationScheme>(nameof(ApiKeyAuthenticationScheme), null);

        services.AddAuthorizationBuilder()
            .AddPolicy(SecurityConstants.Policies.AdminOnly, builder =>
                builder.RequireRole(SecurityConstants.Roles.Admin))
            .AddPolicy(SecurityConstants.Policies.AdminOrUser, builder =>
                builder.RequireRole(SecurityConstants.Roles.Admin, SecurityConstants.Roles.User));

        return services;
    }
}
