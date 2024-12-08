namespace Europhonium.WebApi.Security;

public static class ApiKeySecurityConstants
{
    public const string SchemeName = "ApiKey";
    public const string HttpRequestHeaderName = "X-Api-Key";
    public const string OptionsConfigKey = "ApiKeySecurity";
    public const string DefinitionName = "ApiKey";
}
