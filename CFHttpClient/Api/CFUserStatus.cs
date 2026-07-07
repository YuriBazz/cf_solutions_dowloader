namespace CF_Solution_Downloader.CFHttpClient.Api;

internal class CFUserStatus(string handle, long from = 1, long count = 100, bool includeSources = true)
{
    private string Handle { get; set; } = handle;
    private long From { get; set; } = from;
    private long Count { get; set; } = count;
    private bool IncludeSources { get; set; } = includeSources;

    public override string ToString()
    {
        return $"user.status?handle={Handle}&from={From}&count={Count}&includeSources={IncludeSources}";
    }
}