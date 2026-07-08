using System.Text.Json.Serialization;

namespace CF_Solution_Downloader.CFEntities;

internal record CFParty
{
    public long? ContestId { get; init; }
    [JsonRequired] public List<CFMember>? Members { get; init; }
    [JsonRequired] public string? ParticipantType { get; init; }
    public long? TeamId { get; init; }
    [JsonRequired] public bool Ghost { get; init; }
    public long? Room { get; init; }
    public long? StartTimeSeconds { get; init; }
}