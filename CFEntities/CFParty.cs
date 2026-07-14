using System.Text.Json.Serialization;

namespace CF_Solution_Downloader.CFEntities;

internal record CFParty
{
    [JsonRequired] public List<CFMember>? Members { get; init; }
    [JsonRequired] public CFPartyParticipantType ParticipantType { get; init; }
    [JsonRequired] public bool Ghost { get; init; }
    public long? ContestId { get; init; }
    public long? TeamId { get; init; }
    public long? Room { get; init; }
    public long? StartTimeSeconds { get; init; }
}