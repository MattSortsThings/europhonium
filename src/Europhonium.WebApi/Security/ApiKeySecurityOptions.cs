namespace Europhonium.WebApi.Security;

public sealed record ApiKeySecurityOptions
{
    public string AdminApiKey { get; set; } = string.Empty;

    public string PublicApiKey { get; set; } = string.Empty;
}
