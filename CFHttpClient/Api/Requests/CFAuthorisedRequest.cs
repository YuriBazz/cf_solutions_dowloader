namespace CF_Solution_Downloader.CFHttpClient.Api.Requests;

internal record CFAuthorisedRequest(string handle, string apiKey, IResponsible responsible) : IResponsible
{
    private string Handle { get; } = handle;
    private string ApiKey { get; } = apiKey;
    private IResponsible Request { get; } = responsible;
    
    public IEnumerable<(string param, string value)> GetSpecificPairs()
    {
        yield return ("handle", Handle);
        yield return ("apiKey", ApiKey);
        foreach (var pair in Request.GetSpecificPairs())
        {
            yield return pair;
        }
    }
}