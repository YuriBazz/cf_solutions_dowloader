namespace CF_Solution_Downloader.CFHttpClient;

internal interface IResponsible
{
    IEnumerable<(string param, string value)> GetResponsePairs();
}