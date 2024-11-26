namespace Europhonium.Endpoints.Public.Placeholders;

internal sealed class PlaceholderEndpointGroup : SubGroup<PublicEndpointGroup>
{
    public PlaceholderEndpointGroup()
    {
        Configure("placeholders", _ => { });
    }
}
