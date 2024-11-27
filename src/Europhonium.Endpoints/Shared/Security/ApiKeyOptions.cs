namespace Europhonium.Endpoints.Shared.Security;

public sealed class ApiKeyOptions
{
    public required string AdminApiKey { get; init; }

    public required string PublicApiKey { get; init; }
}
