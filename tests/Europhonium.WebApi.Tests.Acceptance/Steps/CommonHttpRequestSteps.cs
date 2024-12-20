using Europhonium.WebApi.Tests.Acceptance.Utils;
using Reqnroll;

namespace Europhonium.WebApi.Tests.Acceptance.Steps;

[Binding]
public sealed class CommonHttpRequestSteps(IWebApiDriver webApiDriver)
{
    [Given(@"I am using version (\d)\.(\d) of the (\w+) API")]
    public void GivenIAmUsingVersionDDOfTheWApi(int majorVersion, int minorVersion, string apiName)
    {
        webApiDriver.UseApi(apiName, majorVersion, minorVersion);
    }
}
