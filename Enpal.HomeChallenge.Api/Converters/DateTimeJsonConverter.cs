using System.Text.Json;
using System.Text.Json.Serialization;

namespace Enpal.HomeChallenge.Api.Converters;

public sealed class DateTimeJsonConverter: JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var date = reader.GetString();
        return DateTime.Parse(date);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        var isoDate = value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        writer.WriteStringValue(isoDate);
    }
}