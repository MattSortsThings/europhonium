using System.Net;
using Europhonium.Web;
using Microsoft.AspNetCore.Mvc.Testing;
using Testcontainers.MsSql;

namespace Europhonium.WebApi.Tests.Acceptance.Utils;

public sealed class CleanWebAppFixture : WebApplicationFactory<IWebAppAssemblyLocator>, IAsyncLifetime, IWebApiDriver
{
    private static readonly Uri BaseAddress = new("http://localhost:5049");

    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder().Build();

    private string _apiRelativeUri = string.Empty;

    private HttpClient? _httpClient;

    private HttpClient HttpClient => _httpClient ??= CreateClient(new WebApplicationFactoryClientOptions
    {
        AllowAutoRedirect = false,
        BaseAddress = BaseAddress
    });

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
        await _dbContainer.DisposeAsync();
    }

    public void UseApi(string apiName, int majorVersion = 1, int minorVersion = 0)
    {
        _apiRelativeUri = $"{apiName.ToLowerInvariant()}/api/v{majorVersion}.{minorVersion}/";
    }

    public async Task<GETResponse> GETAsync(string route)
    {
        HttpResponseMessage response = await HttpClient.GetAsync(BuildApiRequestUri(route));

        HttpStatusCode statusCode = response.StatusCode;
        var content = await response.Content.ReadAsStringAsync();

        return new GETResponse(statusCode, content);
    }

    public void Reset()
    {
        _apiRelativeUri = string.Empty;
    }

    private Uri BuildApiRequestUri(string route) => new(_apiRelativeUri + route, UriKind.Relative);
}
