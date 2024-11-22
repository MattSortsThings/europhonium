using FastEndpoints;

namespace Europhonium.Endpoints.Admin;

internal sealed class AdminEndpointGroup : Group
{
    internal const string Tag = "AdminEndpointGroup";

    public AdminEndpointGroup()
    {
        Configure("admin", definition =>
        {
            definition.AllowAnonymous();
            definition.Tags(Tag);
        });
    }
}
