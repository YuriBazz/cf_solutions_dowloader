using System.Text.Json.Serialization;

namespace CF_Solution_Downloader.CFEntities;

internal record CFResponse<CFEntity>
{
    [JsonRequired] public CFResponseStatus Status { get; init; } 
    public List<CFEntity>? Result { get; init; }
    public string? Comment { get; init; }
}