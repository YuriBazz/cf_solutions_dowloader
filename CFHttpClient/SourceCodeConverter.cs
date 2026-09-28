using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CF_Solution_Downloader.CFHttpClient;

public class SourceCodeConverter : JsonConverter<string>
{
    public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var base64 = reader.GetString();
        return string.IsNullOrEmpty(base64) ? base64 : Encoding.UTF8.GetString(Convert.FromBase64String(base64));
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        throw new NotImplementedException(); // TODO: Я сомневаюсь, что оно вообще когда-то понадобится для сериализации.
    }
}