using System.Security.Cryptography;
using System.Text;
using System.Xml.Schema;
using CF_Solution_Downloader.CFHttpClient.Api;

namespace CF_Solution_Downloader.CFHttpClient;

internal static class CFSigGenerator
{
// Обозначим их как rand. Остальное содержимое параметра — это шестнадцатеричное представление хэш-кода SHA-512 от следующей строки:
// <rand>/<methodName>?param_1=value_1&param_2=value_2...&param_n=value_n#<secret>
// где (param_1, value_1), (param_2, value_2),..., (param_n, value_n) — это все параметра запроса (включая apiKey и time, но исключая apiSig) с соответствующими значениями отсортированные лексикографически в первую очередь по param_i, во вторую очередь по value_i.

// https://codeforces.com/api/contest.hacks?contestId=566&apiKey=xxx&time=1783504490&apiSig=123456<hash>, где <hash> равен sha512Hex(123456/contest.hacks?apiKey=xxx&contestId=566&time=1783504490#yyy)

    private const string Alphabet = "abcdefghijklmnopqrstuvwxyz1234567890";

    private static char GetRandomChar()
    {
        var rnd = new Random();
        return Alphabet[rnd.Next(0, Alphabet.Length)];
    }

    private static StringBuilder GetRandomBegin()
    {
        StringBuilder sb = new();
        for (int i = 0; i < 6; ++i) sb.Append(GetRandomChar());
        return sb;
    }

    private static StringBuilder GetHash(StringBuilder optionsWithSecret, StringBuilder randomBegin)
    {
        StringBuilder hash = new();
        hash.Append(randomBegin);
        hash.Append('/');
        hash.Append(optionsWithSecret);

        StringBuilder result = new();
        using SHA512 sha512 = SHA512.Create();
        var data = sha512.ComputeHash(Encoding.UTF8.GetBytes(hash.ToString()));
        foreach (var letter in data)
            result.Append(letter.ToString("x2"));

        return result;
    }
    
    public static string GetSignedApiCall(string methodName, IResponsible responsible, string apiSecret)
    {
        var randomBegin = GetRandomBegin();

        StringBuilder resultOptions = new();

        resultOptions.Append(methodName);
        resultOptions.Append('?');
        
        StringBuilder optionWithSecret = new();
        optionWithSecret.Append(resultOptions);
        
        // TODO: Протестить, важен ли им порядок (скорее всего нет, а тогда можно будет не париться с этой лажей)
        resultOptions.AppendJoin('&', responsible.GetResponsePairs().Select(x => x.param +"="+x.value));
        optionWithSecret.AppendJoin('&', responsible.GetResponsePairs().Order(new CFApiComparator()).Select(x =>x.param +"="+x.value));
        
        optionWithSecret.Append('#');
        optionWithSecret.Append(apiSecret);
        var hash = GetHash(optionWithSecret, randomBegin);

        resultOptions.Append("&apiSig=");
        resultOptions.Append(randomBegin);
        resultOptions.Append(hash);
        string result = resultOptions.ToString();
        Console.WriteLine(result);
        return result;
    }
}