using System.Text.Json;
using System.Text.Json.Nodes;
using CF_Solution_Downloader.CFEntities;
using CF_Solution_Downloader.CFHttpClient.Api;
using CF_Solution_Downloader.CFHttpClient.Api.Requests;

namespace CF_Solution_Downloader.CFHttpClient;

internal class Client
{
    private static readonly HttpClient _httpClient = new()
    {
        BaseAddress = new Uri(CFApi.ApiBase),
    };
    
    private readonly bool _authorised;
    private readonly string _handle;
    private readonly string _apiKey;
    private readonly string _apiSecret;

    public Client(string handle = null, string apiKey = null, string apiSecret = null)
    {
        (_handle, _apiKey, _apiSecret) = (handle, apiKey, apiSecret);
        _authorised = _handle is not null;
        HealthCheck();
    }

    private void HealthCheck()
    {
        
    }


    private async Task<CFResponse<ResponseType>> GetAsync<ResponseType>(string optionsString)
    {
        using HttpResponseMessage responseMessage = await _httpClient.GetAsync(optionsString);
        
        try
        {
            responseMessage.EnsureSuccessStatusCode();
            var json = await responseMessage.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CFResponse<ResponseType>>(json, JsonOptions.Options);
        }
        catch (HttpRequestException httpRequestException)
        {
            await Console.Error.WriteLineAsync($"CodeForces returned {responseMessage.StatusCode}");
            await Console.Error.WriteLineAsync(httpRequestException.Message);
            // TODO: Logging for httpRequestException
        }
        catch (Exception exception)
        {
            await Console.Error.WriteLineAsync(exception.Message);
            //NOTE: Реально нужно добавить логгирование
        }

        return null;
    }

    private async Task<CFResponse<CFSubmission>> GetUserStatusAsync(long from, long count, bool includeSources)
    {
        if (!_authorised)
        {
            throw new InvalidOperationException("User have to be authorised");
        }
        var userStatus = new CFAuthorisedRequest(_handle, _apiKey, new CFUserStatusRequest(from, count, includeSources));
        return await GetAsync<CFSubmission>(CFSigGenerator.GetSignedApiCall(CFApi.UserStatusMethod, userStatus, _apiSecret));
    }

    public List<CFSubmission>? GetUSerSubmissions(long from, long count = 100, bool includeSources = true)
    {
        try
        {
            var response = GetUserStatusAsync(from, count, includeSources).Result;
            return response.Result;
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine(exception.Message);
        }

        return null;
    }

    public List<CFSubmission>? GetAllUserSubmissions(bool includeSources = true)
    {
        List<CFSubmission> result = new();
        int expectedSize = 100, actualSize = 100, start = 1;
        while (expectedSize == actualSize)
        {
            var currentList = GetUSerSubmissions(1, expectedSize, includeSources);
            if (currentList is null)
            {
                Console.Error.WriteLine("Can't get submissions");
                return null;
            }
            actualSize = currentList.Count;
            result.AddRange(currentList);
            start += actualSize;
        }

        return result;
    }
}
