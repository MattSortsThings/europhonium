using FastEndpoints;

namespace Europhonium.Endpoints.Public.Placeholders;

internal sealed class PlaceholdersEndpointSubGroup : SubGroup<PublicEndpointGroup>
{
    public PlaceholdersEndpointSubGroup()
    {
        Configure("placeholders", _ => { });
    }
}
