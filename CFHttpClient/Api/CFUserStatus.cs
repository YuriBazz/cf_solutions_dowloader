namespace CF_Solution_Downloader.CFHttpClient.Api;

internal record CFUserStatus(
    string handle,
    string apiKey,
    long from,
    long count,
    bool includeSources) : IResponsible
{
    private string Handle { get; set; } = handle;
    private long From { get; set; } = from;
    private long Count { get; set; } = count;
    private bool IncludeSources { get; set; } = includeSources;

    private string ApiKey { get; set; } = apiKey;

    /*
       apiKey — должен быть равен key
       time — текущее время в формате unix (например, System.currentTimeMillis()/1000).
        Если разница между временем на сервере и временем в параметре будет больше 5 минут, то запрос не будет выполнен.
       apiSig — подпись для того, чтобы убедиться, что вы знаете и key, и secret.
        Первые 6 символов параметра apiSig могут быть произвольными.
        Мы советуем выбирать их случайно при каждом запросе.
        Обозначим их как rand. Остальное содержимое параметра — это шестнадцатеричное представление хэш-кода SHA-512 от следующей строки:
            <rand>/<methodName>?param_1=value_1&param_2=value_2...&param_n=value_n#<secret>
                где (param_1, value_1), (param_2, value_2),..., (param_n, value_n) — это все параметра запроса (включая apiKey и time, но исключая apiSig) с соответствующими значениями отсортированные лексикографически в первую очередь по param_i, во вторую очередь по value_i.

       Например:

       Если ваш key равен xxx, secret равен yyy, выбранный rand равен 123456, и вы хотите отправить запрос методу contest.hacks
        для соревнования 566, то вам надо составить запрос следующим образом: 
            https://codeforces.com/api/contest.hacks?contestId=566&apiKey=xxx&time=1783504490&apiSig=123456<hash>, где <hash> равен sha512Hex(123456/contest.hacks?apiKey=xxx&contestId=566&time=1783504490#yyy)

     */
    public IEnumerable<(string param, string value)> GetResponsePairs()
    {
        List<(string param, string value)> result =
        [
            ("handle", Handle),
            ("from", From.ToString()),
            ("count", Count.ToString()),
            ("includeSources", IncludeSources ? "true" : "false"),
            ("apiKey", ApiKey),
            ("time", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
        ];

        return result;
    }
}