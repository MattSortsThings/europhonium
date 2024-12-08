namespace Europhonium.Shared.Infrastructure.Security;

public static class SecurityConstants
{
    public static class Policies
    {
        public const string AdminOnly = "AdminOnly";
        public const string AdminOrUser = "AdminOrUser";
    }

    public static class ClientIds
    {
        public const string Admin = "Client.Admin";
        public const string User = "Client.User";
        public const string Default = "Client.Default";
    }

    public static class Roles
    {
        public const string Admin = "Admin";
        public const string User = "User";
    }
}
