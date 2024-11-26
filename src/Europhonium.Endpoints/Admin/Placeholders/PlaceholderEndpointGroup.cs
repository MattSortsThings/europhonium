namespace Europhonium.Endpoints.Admin.Placeholders;

internal sealed class PlaceholderEndpointGroup : SubGroup<AdminEndpointGroup>
{
    public PlaceholderEndpointGroup()
    {
        Configure("placeholders", _ => { });
    }
}
