using Europhonium.Shared.Domain.Placeholders.Greetings;

namespace Europhonium.Modules.AdminApi.V1.Placeholders.Greetings;

internal static class DomainMapping
{
    internal static Language ToLanguage(this LanguageOption languageOption) => Enum.Parse<Language>(languageOption.ToString());

    internal static LanguageOption ToLanguageOption(this Language language) => Enum.Parse<LanguageOption>(language.ToString());
}
