using Europhonium.Endpoints.Shared.Security;

namespace Europhonium.Endpoints.Admin;

internal sealed class AdminEndpointGroup : Group
{
    public AdminEndpointGroup()
    {
        Configure("admin", definition =>
        {
            definition.Policies(SecurityConstants.Policies.AdminOnly);
            definition.Tags(nameof(AdminEndpointGroup));
        });
    }
}
