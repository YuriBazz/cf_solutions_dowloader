using System.Text.Json;
using System.Text.Json.Serialization;

namespace CF_Solution_Downloader.CFHttpClient;

internal static class JsonOptions
{
    public static JsonSerializerOptions Options
    {
        get
        {
            if (field is null)
            {
                field = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    Converters =
                    {
                        new JsonStringEnumConverter(),
                    }
                };
            }
            return field;
        }
    } = null;
}