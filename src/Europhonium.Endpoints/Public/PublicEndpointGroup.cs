namespace Europhonium.Endpoints.Public;

internal sealed class PublicEndpointGroup : Group
{
    public PublicEndpointGroup()
    {
        Configure("public", definition =>
        {
            definition.AllowAnonymous();
            definition.Tags(nameof(PublicEndpointGroup));
        });
    }
}
