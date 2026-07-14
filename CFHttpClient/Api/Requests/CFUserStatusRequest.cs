namespace CF_Solution_Downloader.CFHttpClient.Api;

internal record CFUserStatusRequest(
    long from,
    long count,
    bool includeSources) : IResponsible
{
    private long From { get; } = from;
    private long Count { get; } = count;
    private bool IncludeSources { get; } = includeSources;

    public IEnumerable<(string param, string value)> GetSpecificPairs()
    {
        List<(string param, string value)> result =
        [
            ("from", From.ToString()),
            ("count", Count.ToString()),
            ("includeSources", IncludeSources ? "true" : "false"),
        ];

        return result;
    }
}