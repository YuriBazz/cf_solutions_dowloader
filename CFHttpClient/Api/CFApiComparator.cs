namespace CF_Solution_Downloader.CFHttpClient.Api;

internal class CFApiComparator : IComparer<(string param, string value)>
{
    public int Compare((string param, string value) x, (string param, string value) y)
    {
        var item1Comparison = string.Compare(x.param, y.param, StringComparison.Ordinal);
        if (item1Comparison != 0) return item1Comparison;
        return string.Compare(x.value, y.value, StringComparison.Ordinal);
    }
}