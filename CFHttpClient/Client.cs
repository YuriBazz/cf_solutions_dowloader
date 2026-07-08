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

    private async Task<JsonArray?> GetAsync(string optionsString)
    {
        using HttpResponseMessage responseMessage = await _httpClient.GetAsync(optionsString);
        
        responseMessage.WriteRequestToConsole();
        
        try
        {
            responseMessage.EnsureSuccessStatusCode();

            var jsonNode = JsonNode.Parse(await responseMessage.Content.ReadAsStringAsync());
            return CFResponseParser.ParseCfResponseJson(jsonNode);
        }
        catch (HttpRequestException httpRequestException)
        {
            await Console.Error.WriteLineAsync($"CodeForces returned {responseMessage.StatusCode}");
            // TODO: Logging for httpRequestException
        }
        catch (Exception exception)
        {
            await Console.Error.WriteLineAsync(exception.Message);
            //NOTE: Реально нужно добавить логгирование
        }

        return null;
    }

    public async Task<JsonArray?> GetUserStatusAsync(long from, long count = 100, bool includeSources = true)
    {
        var userStatus = new CFUserStatus(_handle, _apiKey, from, count, includeSources);
        return await GetAsync(CFSigGenerator.GetSignedApiCall(CFApi.UserStatusMethod, userStatus, _apiSecret));
    }
}
static class HttpResponseMessageExtensions
{
    internal static void WriteRequestToConsole(this HttpResponseMessage response)
    {
        if (response is null)
        {
            return;
        }

        var request = response.RequestMessage;
        Console.Write($"{request?.Method} ");
        Console.Write($"{request?.RequestUri} ");
        Console.WriteLine($"HTTP/{request?.Version}");        
    }
}
