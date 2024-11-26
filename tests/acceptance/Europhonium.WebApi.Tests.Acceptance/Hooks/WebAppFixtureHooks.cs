using Europhonium.WebApi.Tests.Acceptance.Drivers;
using Europhonium.WebApi.Tests.Acceptance.Utils.Fixtures;
using Reqnroll.BoDi;

namespace Europhonium.WebApi.Tests.Acceptance.Hooks;

[Binding]
public static class WebAppFixtureHooks
{
    [BeforeTestRun]
    public static async Task InitializeWebAppFixtureAsync(IObjectContainer container)
    {
        WebAppFixture fixture = new();
        await fixture.InitializeAsync();

        container.RegisterInstanceAs(fixture);
        container.RegisterInstanceAs(fixture, typeof(IHttpClientProvider));
    }

    [AfterTestRun]
    public static async Task DisposeOfWebAppFixtureAsync(IObjectContainer container)
    {
        var fixture = container.Resolve<WebAppFixture>();
        await fixture.DisposeAsync();
    }
}
