using System.Text.Json.Serialization;

namespace CF_Solution_Downloader.CFEntities;

internal class CFSubmission
{
    [JsonRequired] public long Id { get; init; }

    [JsonRequired] public long RelativeTimeSeconds { get; init; }

    [JsonRequired] public CFProblem Problem { get; init; }

    [JsonRequired] public CFParty Author { get; init; }

    [JsonRequired] public string ProgrammingLanguage { get; init; }

    [JsonRequired] public long PassedTestCount { get; init; }

    [JsonRequired] public long TimeConsumedMillis { get; init; }

    [JsonRequired] public long MemoryConsumedBytes { get; init; }

    public double? Points { get; init; }
    public string? Verdict { get; init; }
    public string? TestSet { get; init; }
    public long? ContestId { get; init; }
    public string? Source { get; init; }
}