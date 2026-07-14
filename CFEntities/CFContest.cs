using System.Text.Json.Serialization;

namespace CF_Solution_Downloader.CFEntities;

internal record CFContest
{
    [JsonRequired] public long Id { get; init; }
    [JsonRequired] public string? Name { get; init; }
    [JsonRequired] public CFContestType Type { get; init; } 
    [JsonRequired] public CFContestPhase Phase { get; init; }
    [JsonRequired] public bool Frozen { get; init; }
    [JsonRequired] public long DurationSeconds { get; init; }
    public long? FreezeDurationSeconds { get; init; }
    public long? StartTimeSeconds { get; init; }
    public long? RelativeTimeSeconds { get; init; }
    public string? PreparedBy { get; init; }
    public string? WebsiteUrl { get; init; }
    public string? Description { get; init; }
    public int? Difficulty { get; init; }
    public string? Kind { get; init; }
    public string? IcpcRegion { get; init; }
    public string? Country { get; init; }
    public string? City { get; init; }
    public string? Season { get; init; }
}