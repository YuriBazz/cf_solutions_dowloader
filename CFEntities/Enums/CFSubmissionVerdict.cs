using System.Text.Json.Serialization;

namespace CF_Solution_Downloader.CFEntities;

[JsonConverter(typeof(JsonStringEnumConverter))]
internal enum CFSubmissionVerdict
{
    OK,
    FAILED,
    PARTIAL,
    COMPILATION_ERROR,
    RUNTIME_ERROR,
    WRONG_ANSWER,
    TIME_LIMIT_EXCEEDED,
    MEMORY_LIMIT_EXCEEDED,
    IDLENESS_LIMIT_EXCEEDED,
    SECURITY_VIOLATED,
    CRASHED,
    INPUT_PREPARATION_CRASHED,
    CHALLENGED,
    SKIPPED,
    TESTING,
    REJECTED,
    SUBMITTED,
}