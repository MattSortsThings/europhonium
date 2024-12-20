using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Europhonium.Apis.Admin.V1.Tests.Integration.Utils;

[Collection(nameof(CleanWebAppTestCollection))]
public abstract class CleanWebAppTestBase : IDisposable
{
    private readonly IServiceScope _scope;
    private readonly CleanWebAppFixture _webAppFixture;
    private ISender? _sender;

    protected CleanWebAppTestBase(CleanWebAppFixture webAppFixture)
    {
        _webAppFixture = webAppFixture;
        _scope = webAppFixture.Services.CreateScope();
    }

    private protected ISender Sender => _sender ??= _scope.ServiceProvider.GetRequiredService<ISender>();

    public void Dispose()
    {
        _webAppFixture.Reset();
        GC.SuppressFinalize(this);
    }
}
