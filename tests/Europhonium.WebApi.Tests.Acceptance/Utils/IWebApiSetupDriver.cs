using Europhonium.Modules.Admin.Countries;

namespace Europhonium.WebApi.Tests.Acceptance.Utils;

public interface IWebApiSetupDriver
{
    /// <summary>
    ///     Creates a country in the web application and returns its ID.
    /// </summary>
    /// <param name="request">Specifies the country to be created.</param>
    /// <returns>A new <see cref="CreatedCountryTag" /> instance containing identifiers for the created country.</returns>
    public Task<CreatedCountryTag> CreateCountryAsync(CreateCountry.Request request);
}
