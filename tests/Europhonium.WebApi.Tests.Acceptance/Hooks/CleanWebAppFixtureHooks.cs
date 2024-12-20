using Europhonium.WebApi.Tests.Acceptance.Utils;
using Reqnroll;
using Reqnroll.BoDi;

namespace Europhonium.WebApi.Tests.Acceptance.Hooks;

[Binding]
public static class CleanWebAppFixtureHooks
{
    [BeforeTestRun]
    public static async Task InitializeCleanWebAppFixtureAsync(IObjectContainer container)
    {
        CleanWebAppFixture fixture = new();
        await fixture.InitializeAsync();

        container.RegisterInstanceAs(fixture);
        container.RegisterInstanceAs<IWebApiDriver>(fixture);
    }

    [BeforeScenario]
    public static void ResetCleanWebAppFixture(IObjectContainer container)
    {
        var fixture = container.Resolve<CleanWebAppFixture>();

        fixture.Reset();
    }

    [AfterTestRun]
    public static async Task DisposeOfCleanWebAppFixtureAsync(IObjectContainer container)
    {
        var fixture = container.Resolve<CleanWebAppFixture>();

        await fixture.DisposeAsync();
    }
}
