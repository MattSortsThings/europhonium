using System.Text.Json.Serialization;

namespace Europhonium.Modules.Admin.Placeholders;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Language
{
    English = 0,
    French = 1,
    Dutch = 2
}
