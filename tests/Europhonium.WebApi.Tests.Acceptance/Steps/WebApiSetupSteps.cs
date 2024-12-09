using Europhonium.Modules.Admin.Countries;
using Europhonium.WebApi.Tests.Acceptance.Utils;
using Reqnroll;

namespace Europhonium.WebApi.Tests.Acceptance.Steps;

[Binding]
public sealed class WebApiSetupSteps(ScenarioContext scenarioContext, IWebApiSetupDriver webApiSetupDriver)
{
    [Given("the following countries exist")]
    public async Task GivenTheFollowingCountriesExist(Table table)
    {
        foreach (CreateCountry.Request? request in table.CreateSet<CreateCountry.Request>())
        {
            (var countryCode, Guid countryId) = await webApiSetupDriver.CreateCountryAsync(request);

            scenarioContext.Add(countryCode, countryId);
        }
    }
}
