using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LemonsTiming24.Server.Infrastructure.JsonConverters;

public class ZeroMeansNullToStringJsonConverter : JsonConverter<string?>

{
    public ZeroMeansNullToStringJsonConverter()
    {
    }

    public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            return reader.GetString();
        }
        else if (reader.TokenType == JsonTokenType.Number)
        {
            var nextValue = reader.GetUInt16();
            if (nextValue==0)
            {
                return null;
            }
            else
            {
                throw new JsonException("Ubnexpoected numeric value");
            }
        }
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, string? value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}

