namespace Europhonium.Apis.Admin.V1.Placeholders.Greetings;

public record GreetingDto(string Message, LanguageOption Language, Guid RequestId);
