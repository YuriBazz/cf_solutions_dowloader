using System.Text.Json.Serialization;

namespace CF_Solution_Downloader.CFEntities;

[JsonConverter(typeof(JsonStringEnumConverter))]
internal enum CFResponseStatus
{
    OK,
    FAILED,
}