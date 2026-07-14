using System.Text.Json.Serialization;

namespace CF_Solution_Downloader.CFEntities;

[JsonConverter(typeof(JsonStringEnumConverter))]
internal enum CFPartyParticipantType
{
    CONTESTANT,
    PRACTICE,
    VIRTUAL,
    MANAGER,
    OUT_OF_COMPETITION,
}