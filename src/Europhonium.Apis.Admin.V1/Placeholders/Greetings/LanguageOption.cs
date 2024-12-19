using System.Text.Json.Serialization;

namespace Europhonium.Apis.Admin.V1.Placeholders.Greetings;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum LanguageOption
{
    English = 0,
    French = 1,
    Dutch = 2
}
