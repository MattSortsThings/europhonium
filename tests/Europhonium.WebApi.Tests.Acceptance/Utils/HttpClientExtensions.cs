using System.Net;

namespace Europhonium.WebApi.Tests.Acceptance.Utils;

public static class HttpClientExtensions
{
    public static async Task<GetResponse> GETAsync(this HttpClient httpClient, string route)
    {
        HttpResponseMessage? response = await httpClient.GetAsync(route);
        HttpStatusCode statusCode = response.StatusCode;
        var content = await response.Content.ReadAsStringAsync();

        return new GetResponse(statusCode, content);
    }
}
