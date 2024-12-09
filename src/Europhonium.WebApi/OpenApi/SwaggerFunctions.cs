namespace Europhonium.WebApi.OpenApi;

internal static class SwaggerFunctions
{
    internal static readonly Func<Type, string> SchemaIdGenerator = type =>
    {
        if (type.IsNested)
        {
            return type.FullName?.Split('.').Last().Replace("+", string.Empty) ?? type.Name;
        }

        return type.Name.EndsWith("Resource") ? type.Name[..^8] : type.Name;
    };
}
