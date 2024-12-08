using Europhonium.Modules.Admin;
using Europhonium.Modules.Public;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Europhonium.WebApi.Security;

/// <summary>
///     Extension methods to be invoked at application startup.
/// </summary>
internal static class DependencyInjection
{
    /// <summary>
    ///     Registers the API key security services for the web application.
    /// </summary>
    /// <param name="services">Contains service descriptors for the web application.</param>
    /// <param name="configuration">Contains configuration properties for the web application.</param>
    /// <returns>The same <see cref="IServiceCollection" /> instance, so that method invocations can be chained.</returns>
    internal static IServiceCollection AddApiKeySecurity(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApiKeySecurityOptions>(configuration.GetSection(ApiKeySecurityConstants.OptionsConfigKey));
        services.AddSingleton<IOptionsMonitor<ApiKeySecurityOptions>, OptionsMonitor<ApiKeySecurityOptions>>();

        services.AddAuthentication(ApiKeySecurityConstants.SchemeName)
            .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationScheme>(ApiKeySecurityConstants.SchemeName, null);

        services.AddAuthorizationBuilder().AddPublicModuleSecurityPolicy().AddAdminModuleSecurityPolicy();

        return services;
    }
}
