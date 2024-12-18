namespace Europhonium.Modules.AdminApi.V1.Placeholders.Greetings;

public sealed record GreetingDto(string Message, LanguageOption Language, Guid GreetingId);
