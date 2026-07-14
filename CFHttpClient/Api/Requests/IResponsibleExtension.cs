namespace CF_Solution_Downloader.CFHttpClient;

internal static class IResponsibleExtension
{
    public static IEnumerable<(string param, string value)> GetResponsePairs(this IResponsible responsible)
    {
        
        foreach (var pair in responsible.GetSpecificPairs())
        {
            yield return pair;
        }

        yield return ("time", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString());
    }
}