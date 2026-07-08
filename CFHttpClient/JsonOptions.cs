using System.Text.Json;

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
                };
            }
            return field;
        }
    } = null;
}