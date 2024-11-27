namespace Europhonium.Endpoints.Admin;

internal sealed class AdminEndpointGroup : Group
{
    public AdminEndpointGroup()
    {
        Configure("admin", definition =>
        {
            definition.AllowAnonymous();
            definition.Tags(nameof(AdminEndpointGroup));
        });
    }
}
