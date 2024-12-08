using Europhonium.Modules.Public;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Europhonium.WebApi.Security;

/// <summary>
///     Extension methods to be invoked at application startup.
/// </summary>
internal static class DependencyInjection
{
    public static IServiceCollection AddApiKeySecurity(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApiKeySecurityOptions>(configuration.GetSection(ApiKeySecurityConstants.OptionsConfigKey));
        services.AddSingleton<IOptionsMonitor<ApiKeySecurityOptions>, OptionsMonitor<ApiKeySecurityOptions>>();

        services.AddAuthentication(ApiKeySecurityConstants.SchemeName)
            .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationScheme>(ApiKeySecurityConstants.SchemeName, null);

        services.AddAuthorizationBuilder().AddPublicModuleSecurityPolicy();


        return services;
    }
}
