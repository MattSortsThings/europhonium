using Europhonium.Domain.Placeholders.Greetings;

namespace Europhonium.Apis.Admin.V1.Placeholders.Greetings;

internal static class DomainMapping
{
    internal static Language ToDomainLanguage(this LanguageOption language) => Enum.Parse<Language>(language.ToString());

    internal static LanguageOption ToApiLanguage(this Language language) => Enum.Parse<LanguageOption>(language.ToString());
}
