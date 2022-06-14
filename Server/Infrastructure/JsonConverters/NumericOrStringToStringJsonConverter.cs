using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LemonsTiming24.Server.Infrastructure.JsonConverters;

public class NumericOrStringToStringJsonConverter : JsonConverter<string?>

{
    public NumericOrStringToStringJsonConverter()
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
            return reader.GetInt32().ToString(CultureInfo.InvariantCulture);
        }

        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, string? value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}

