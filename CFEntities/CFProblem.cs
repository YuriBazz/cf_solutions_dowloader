using System.Text.Json.Serialization;

namespace CF_Solution_Downloader.CFEntities;


internal record CFProblem
{
    public long ContestId { get; init; }
    public string? ProblemSetName { get; init; }
    [JsonRequired] public string? Index { get; init; }
    [JsonRequired] public string? Name { get; init; }
    [JsonRequired] public string? Type { get; init; }
    public double? Points { get; init; }
    public long? Rating { get; init; }
    [JsonRequired] public List<string>? Tags { get; init; }
}