using System.Text.Json.Serialization;

namespace CF_Solution_Downloader.CFEntities;

public record CFMember
{
    [JsonRequired] public string? Handle { get; init; }
    public string? Name { get; init; }
}