namespace CF_Solution_Downloader;

internal static class Program
{
    internal static void Main(string[] args)
    {
        var client = new CFHttpClient.Client("YuriBazz", "",
            "", null, 12142);

        var test = client.GetUserStatusAsync(1, 3, false).Result;
        if (test is null)
        {
            Console.WriteLine("Returned null");
            return;
        }
        
        Console.WriteLine(test.ToString());
    }
}
