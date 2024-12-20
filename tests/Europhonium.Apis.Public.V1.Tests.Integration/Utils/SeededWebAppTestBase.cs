using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Europhonium.Apis.Public.V1.Tests.Integration.Utils;

[Collection(nameof(SeededWebAppTestCollection))]
public abstract class SeededWebAppTestBase
{
    private readonly IServiceScope _scope;
    private readonly SeededWebAppFixture _webAppFixture;
    private ISender? _sender;

    protected SeededWebAppTestBase(SeededWebAppFixture webAppFixture)
    {
        _webAppFixture = webAppFixture;
        _scope = webAppFixture.Services.CreateScope();
    }

    private protected ISender Sender => _sender ??= _scope.ServiceProvider.GetRequiredService<ISender>();
}
