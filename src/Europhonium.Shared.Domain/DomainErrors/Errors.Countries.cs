using ErrorOr;
using Europhonium.Shared.Domain.Countries;

namespace Europhonium.Shared.Domain.DomainErrors;

public static class Errors
{
    public static class Countries
    {
        public static Error InvalidCountryCode(string countryCode) =>
            Error.Validation("Countries.InvalidCountryCode",
                "Country code must be a string of 2 upper-case letters.",
                new Dictionary<string, object> { { nameof(countryCode), countryCode } });

        public static Error CountryCodeConflict(string countryCode) =>
            Error.Conflict("Countries.CountryCodeCode",
                "A country with the specified country code already exists.",
                new Dictionary<string, object> { { nameof(countryCode), countryCode } });

        public static Error CountryNotFound(CountryId countryId) =>
            Error.NotFound("Countries.CountryNotFound",
            "No country exists with the specified ID.",
            new Dictionary<string, object> { { nameof(countryId), countryId.Value } });
    }
}
