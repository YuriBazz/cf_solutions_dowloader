using System.Text.Json;
using System.Text.Json.Nodes;
using CF_Solution_Downloader.CFEntities;
using CF_Solution_Downloader.CFHttpClient.Api;

namespace CF_Solution_Downloader.CFHttpClient;

internal class Client
{
    private static readonly HttpClient _httpClient = new()
    {
        BaseAddress = new Uri(CFApi.ApiBase),
    };

    private readonly string _handle;
    private readonly string _apiKey;
    private readonly string _apiSecret;
    private readonly HashSet<long> _ids;

    public Client(string handle, string apiKey, string apiSecret, List<string>? groupCodes, List<long>? contestIds)
    {
        (_handle, _apiKey, _apiSecret) = (handle, apiKey, apiSecret);
        if (groupCodes is null && contestIds is null)
        {
            Console.Error.WriteLine("groupCode and contestId were absent");
            throw new ArgumentException();
        }

        _ids = new();
        if (contestIds is not null)
        {
            foreach (var contestId in contestIds)
            {
                _ids.Add(contestId);
            }
        }
    }

    private async Task<JsonArray?> GetAsync(string optionsString)
    {
        using HttpResponseMessage responseMessage = await _httpClient.GetAsync(optionsString);
        
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

    private async Task<JsonArray?> GetUserStatusAsync(long from, long count, bool includeSources)
    {
        var userStatus = new CFUserStatus(_handle, _apiKey, from, count, includeSources);
        return await GetAsync(CFSigGenerator.GetSignedApiCall(CFApi.UserStatusMethod, userStatus, _apiSecret));
    }

    public List<CFSubmission>? GetUSerSubmissions(long from, long count = 100, bool includeSources = true)
    {
        var jsonArray = GetUserStatusAsync(from, count, includeSources).Result;
        return jsonArray.Deserialize<List<CFSubmission>>(JsonOptions.Options);
    }
}
