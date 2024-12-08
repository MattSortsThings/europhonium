using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;
using Europhonium.Shared.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace Europhonium.WebApi.Security;

/// <summary>
///     Role-based API key authentication scheme.
/// </summary>
/// <remarks>
///     This class is adapted from the FastEndpoints
///     <a href="https://gist.github.com/dj-nitehawk/4efe5ef70f813aec2c55fff3bbb833c0">API key authentication example</a>
///     by DJ-Nitehawk.
/// </remarks>
internal sealed class ApiKeyAuthenticationScheme : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IOptionsMonitor<ApiKeySecurityOptions> _apiKeySecurityOptions;

    /// <summary>
    ///     Creates and returns a new <see cref="ApiKeyAuthenticationScheme" /> instance.
    /// </summary>
    /// <param name="apiKeySecurityOptions">Monitors API key security options.</param>
    /// <param name="authenticationSchemeOptions">Monitors authentication scheme options.</param>
    /// <param name="logger">Configures and creates loggers.</param>
    /// <param name="encoder">URL character encoding.</param>
    public ApiKeyAuthenticationScheme(IOptionsMonitor<ApiKeySecurityOptions> apiKeySecurityOptions,
        IOptionsMonitor<AuthenticationSchemeOptions> authenticationSchemeOptions,
        ILoggerFactory logger,
        UrlEncoder encoder) : base(authenticationSchemeOptions, logger, encoder)
    {
        _apiKeySecurityOptions = apiKeySecurityOptions;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        AuthenticateResult result;

        if (IsSwaggerOrFaviconRoute())
        {
            result = BypassAuthorization();
        }
        else
        {
            Request.Headers.TryGetValue(ApiKeySecurityConstants.HttpRequestHeaderName, out StringValues extractedApiKey);

            result = extractedApiKey.Equals(GetPublicApiKey())
                ? AuthenticateAsUser()
                : extractedApiKey.Equals(GetAdminApiKey())
                    ? AuthenticateAsAdmin()
                    : AuthenticateResult.Fail("Valid API key not present in request headers");
        }

        return Task.FromResult(result);
    }

    private StringValues GetAdminApiKey() => _apiKeySecurityOptions.CurrentValue.AdminApiKey;

    private StringValues GetPublicApiKey() => _apiKeySecurityOptions.CurrentValue.PublicApiKey;

    private bool IsSwaggerOrFaviconRoute() =>
        Request.Path.StartsWithSegments("/swagger") || Request.Path.StartsWithSegments("/favicon.ico");

    private AuthenticateResult BypassAuthorization()
    {
        ClaimsIdentity identity = new([new Claim("ClientID", SecurityConstants.ClientIds.Default)], Scheme.Name);
        GenericPrincipal principal = new(identity, null);
        AuthenticationTicket ticket = new(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }

    private AuthenticateResult AuthenticateAsAdmin()
    {
        ClaimsIdentity identity = new([new Claim("ClientID", SecurityConstants.ClientIds.Admin)], Scheme.Name);
        GenericPrincipal principal = new(identity, [SecurityConstants.Roles.Admin]);
        AuthenticationTicket ticket = new(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }

    private AuthenticateResult AuthenticateAsUser()
    {
        ClaimsIdentity identity = new([new Claim("ClientID", SecurityConstants.ClientIds.User)], Scheme.Name);
        GenericPrincipal principal = new(identity, [SecurityConstants.Roles.User]);
        AuthenticationTicket ticket = new(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}
