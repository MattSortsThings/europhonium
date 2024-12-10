using System.Net;
using Europhonium.WebApi.Tests.Acceptance.Utils;
using Reqnroll;

namespace Europhonium.WebApi.Tests.Acceptance.Steps;

public sealed partial class HttpRequestSteps
{
    [When("I request all the queryable countries")]
    public async Task WhenIRequestAllTheQueryableCountries()
    {
        const string route = "api/v1/public/queryables/countries";

        (HttpStatusCode statusCode, var content) = await HttpClient.GETAsync(route);

        scenarioContext.Add(ScenarioKeys.HttpResponse.StatusCode, statusCode);
        scenarioContext.Add(ScenarioKeys.HttpResponse.Content, content);
    }
}
