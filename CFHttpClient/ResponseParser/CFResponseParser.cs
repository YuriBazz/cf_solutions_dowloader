using System.Text.Json;
using System.Text.Json.Nodes;
using CF_Solution_Downloader.CFEntities;

namespace CF_Solution_Downloader.CFHttpClient;

internal static class CFResponseParser
{
    public static JsonArray ParseCfResponseJson(JsonNode? jsonNode)
    {
        if (jsonNode is null) throw new ArgumentNullException(nameof(jsonNode), "Was null");
        var jsonObject = (JsonObject)jsonNode;
        var statusNode = jsonObject["status"];
        if (statusNode is null) throw new JsonException($"{nameof(statusNode)} was null");
        var statusValue = ((JsonValue)statusNode).GetValue<CFResponseStatus>();

        if (statusValue == CFResponseStatus.FAILED)
        {
            var commentNode = jsonObject["comment"];
            if (commentNode is null) throw new JsonException($"{nameof(commentNode)} wa null");
            throw new JsonException("Comment: " + ((JsonValue)commentNode).GetValue<string>());
        }
        var resultNode = jsonObject["result"];
        if (resultNode is null) throw new JsonException($"{nameof(resultNode)} was null");
        return (JsonArray)resultNode;
    }
}