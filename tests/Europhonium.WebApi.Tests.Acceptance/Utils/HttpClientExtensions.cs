using System.Net;
using System.Text;

namespace Europhonium.WebApi.Tests.Acceptance.Utils;

public static class HttpClientExtensions
{
    public static async Task<GETResponse> GETAsync(this HttpClient httpClient, string route)
    {
        HttpResponseMessage response = await httpClient.GetAsync(route);

        HttpStatusCode statusCode = response.StatusCode;
        var content = await response.Content.ReadAsStringAsync();

        return new GETResponse(statusCode, content);
    }

    public static async Task<POSTResponse> POSTAsync(this HttpClient httpClient, string route, string jsonContent)
    {
        var requestBody = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await httpClient.PostAsync(route, requestBody);

        HttpStatusCode statusCode = response.StatusCode;
        var content = await response.Content.ReadAsStringAsync();
        var location = response.Headers.Location?.ToString();

        return new POSTResponse(statusCode, content, location);
    }
}
