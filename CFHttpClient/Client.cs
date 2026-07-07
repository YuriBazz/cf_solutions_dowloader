using System.Text.Json.Nodes;
using CF_Solution_Downloader.CFHttpClient.Api;

namespace CF_Solution_Downloader.CFHttpClient;

internal class Client
{
    private static HttpClient _httpClient;

    private readonly string _handle;
    private readonly string _apiKey;
    private readonly string _apiSecret;
    /// <summary>
    /// If it's not equal to null, then the program will download ACs from all group's contests.
    /// </summary>
    private readonly string? _groupCode;
    private readonly long? _contestId;

    public Client(string handle, string apiKey, string apiSecret, string? groupCode, long? contestId)
    {
        (_handle, _apiKey, _apiSecret, _groupCode, _contestId) = (handle, apiKey, apiSecret, groupCode, contestId);
        if (_groupCode is null && _contestId is null)
        {
            Console.Error.WriteLine("groupCode and contestId were absent");
            throw new ArgumentException();
        }

        _httpClient = new()
        {
            BaseAddress = new Uri(CFApi.ApiBase),
        };
    }


    private async Task<string> GetUserStatusAsync(long from)
    {
        var userStatus = new CFUserStatus(_handle, from);
        
        using HttpResponseMessage response = await _httpClient.GetAsync(userStatus.ToString());
        // TODO: Realise POCO for UserStatus response -> { "status" : "...", "result" : Array of Submissions}
        return "";
    }
    
}