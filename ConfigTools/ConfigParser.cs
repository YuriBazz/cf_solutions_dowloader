using System.Text.Json;
using CF_Solution_Downloader.CFHttpClient;

namespace CF_Solution_Downloader.ConfigTools;

internal static class ConfigParser
{
    public static UserConfig ParseJsonConfig(string path)
    {
        using var configFile = File.OpenText(path);
        return JsonSerializer.Deserialize<UserConfig>(configFile.ReadToEnd(), JsonOptions.Options);
    }

    public static UserConfig ParseEnvConfig()
    {
        string?
            handle = Environment.GetEnvironmentVariable("CF_HANDLE"),
            apiKey = Environment.GetEnvironmentVariable("CF_API_KEY"),
            apiSecret = Environment.GetEnvironmentVariable("CF_API_SECRET"),
            folder = Environment.GetEnvironmentVariable("CF_FOLDER_PATH")
            ;

        // TODO: 0_0
        if (handle is null)
        {
            Console.Error.WriteLine("CF_HANDLE env has not set");
            return null;
        }
        if (apiKey is null)
        {
            Console.Error.WriteLine("CF_API_KEY env has not set");
            return null;
        }
        if (apiSecret is null)
        {
            Console.Error.WriteLine("CF_API_SECRET env has not set");
            return null;
        }

        return new UserConfig
        {
            Handle = handle, ApiKey = apiKey, ApiSecret = apiSecret, FolderPath = folder
        };
    }
}