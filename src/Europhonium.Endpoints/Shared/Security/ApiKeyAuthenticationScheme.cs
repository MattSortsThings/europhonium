using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace Europhonium.Endpoints.Shared.Security;

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
    private readonly IOptionsMonitor<ApiKeyOptions> _apiKeyOptions;

    /// <summary>
    ///     Creates and returns a new <see cref="ApiKeyAuthenticationScheme" /> instance.
    /// </summary>
    /// <param name="options">Monitors authentication scheme options.</param>
    /// <param name="logger">Configures and creates loggers.</param>
    /// <param name="encoder">URL character encoding.</param>
    /// <param name="apiKeyOptions">Monitors API key options.</param>
    public ApiKeyAuthenticationScheme(IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IOptionsMonitor<ApiKeyOptions> apiKeyOptions) : base(options, logger, encoder)
    {
        _apiKeyOptions = apiKeyOptions;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        AuthenticateResult result;

        if (IsSwaggerEndpoint())
        {
            result = BypassAuthorization();
        }
        else
        {
            Request.Headers.TryGetValue(SecurityConstants.ApiKeyRequestHeaderName, out StringValues extractedApiKey);

            result = extractedApiKey.Equals(_apiKeyOptions.CurrentValue.PublicApiKey)
                ? AuthenticateAsUser()
                : extractedApiKey.Equals(_apiKeyOptions.CurrentValue.AdminApiKey)
                    ? AuthenticateAsAdmin()
                    : AuthenticateResult.Fail("Valid API key not present in request headers");
        }

        return Task.FromResult(result);
    }

    private bool IsSwaggerEndpoint() => Request.Path.StartsWithSegments("/swagger");

    private AuthenticateResult BypassAuthorization()
    {
        ClaimsIdentity identity = new([new Claim("ClientID", "default")], Scheme.Name);
        GenericPrincipal principal = new(identity, null);
        AuthenticationTicket ticket = new(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }

    private AuthenticateResult AuthenticateAsAdmin()
    {
        ClaimsIdentity identity = new([new Claim("ClientID", SecurityConstants.Roles.Admin)], Scheme.Name);
        GenericPrincipal principal = new(identity, [SecurityConstants.Roles.Admin]);
        AuthenticationTicket ticket = new(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }

    private AuthenticateResult AuthenticateAsUser()
    {
        ClaimsIdentity identity = new([new Claim("ClientID", SecurityConstants.Roles.User)], Scheme.Name);
        GenericPrincipal principal = new(identity, [SecurityConstants.Roles.User]);
        AuthenticationTicket ticket = new(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}
