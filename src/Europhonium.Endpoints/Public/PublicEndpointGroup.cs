using Europhonium.Endpoints.Shared.Security;

namespace Europhonium.Endpoints.Public;

internal sealed class PublicEndpointGroup : Group
{
    public PublicEndpointGroup()
    {
        Configure("public", definition =>
        {
            definition.Policies(SecurityConstants.Policies.AdminOrUser);
            definition.Tags(nameof(PublicEndpointGroup));
        });
    }
}
