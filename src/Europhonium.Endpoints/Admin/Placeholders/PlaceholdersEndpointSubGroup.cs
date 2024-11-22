using FastEndpoints;

namespace Europhonium.Endpoints.Admin.Placeholders;

internal sealed class PlaceholdersEndpointSubGroup : SubGroup<AdminEndpointGroup>
{
    public PlaceholdersEndpointSubGroup()
    {
        Configure("placeholders", _ => { });
    }
}
