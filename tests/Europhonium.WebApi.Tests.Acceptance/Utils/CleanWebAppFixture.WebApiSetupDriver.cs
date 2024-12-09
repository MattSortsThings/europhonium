using System.Net.Http.Json;
using Europhonium.Modules.Admin.Countries;

namespace Europhonium.WebApi.Tests.Acceptance.Utils;

public sealed partial class CleanWebAppFixture : IWebApiSetupDriver
{
    public async Task<CreatedCountryTag> CreateCountryAsync(CreateCountry.Request request)
    {
        HttpClient client = GetClientUsingAdminApiKey();
        HttpResponseMessage response = await client.PostAsJsonAsync("api/v1/admin/countries", request);

        var responseObject = await response.Content.ReadFromJsonAsync<CreateCountry.Response>();

        (Guid id, var countryCode, _, _, _, _) = responseObject!.Country;

        return new CreatedCountryTag(countryCode, id);
    }
}
