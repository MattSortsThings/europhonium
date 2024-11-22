using FastEndpoints;

namespace Europhonium.Endpoints.Public;

internal sealed class PublicEndpointGroup : Group
{
    internal const string Tag = "PublicEndpointGroup";

    public PublicEndpointGroup()
    {
        Configure("public", definition =>
        {
            definition.AllowAnonymous();
            definition.Tags(Tag);
        });
    }
}
