using System.Net;
using Reqnroll;

namespace Europhonium.WebApi.Tests.Acceptance.Utils;

internal static class ScenarioContextExtensions
{
    private const string ResponseStatusCode = "ResponseStatusCode";
    private const string ResponseContent = "ResponseContent";

    internal static void AddResponseStatusCode(this ScenarioContext scenarioContext, HttpStatusCode statusCode) =>
        scenarioContext.Add(ResponseStatusCode, statusCode);

    internal static HttpStatusCode GetResponseStatusCode(this ScenarioContext scenarioContext) =>
        scenarioContext.Get<HttpStatusCode>(ResponseStatusCode);

    internal static void AddResponseContent(this ScenarioContext scenarioContext, string content) =>
        scenarioContext.Add(ResponseContent, content);

    internal static string GetResponseContent(this ScenarioContext scenarioContext) =>
        scenarioContext.Get<string>(ResponseContent);
}
