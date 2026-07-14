using System.Text.Json.Serialization;

namespace CF_Solution_Downloader.CFEntities;

[JsonConverter(typeof(JsonStringEnumConverter))]
internal enum CFContestPhase
{
    BEFORE,
    CODING,
    PENDING_SYSTEM_TEST,
    SYSTEM_TEST,
    FINISHED,
}