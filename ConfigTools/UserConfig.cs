using System.Text.Json.Serialization;

namespace CF_Solution_Downloader.ConfigTools;

internal record UserConfig
{
    [JsonRequired] public string Handle { get; init; }
    [JsonRequired] public string ApiKey { get; init; }
    [JsonRequired] public string ApiSecret { get; init; }
    
    public string? FolderPath { get; init; }
}