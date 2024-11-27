namespace Europhonium.Endpoints.Shared.Security;

public static class SecurityConstants
{
    public const string ApiKeyRequestHeaderName = "X-Api-Key";

    internal static class Policies
    {
        internal const string AdminOnly = "AdminOnly";
        internal const string AdminOrUser = "AdminOrUser";
    }

    internal static class Roles
    {
        internal const string Admin = "admin";
        internal const string User = "user";
    }
}
