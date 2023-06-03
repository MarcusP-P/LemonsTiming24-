
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LemonsTiming24.Server.Model.RawTiming;

public class BestSector
{
    [JsonPropertyName("driver")]
    public int Driver { get; set; }
    [JsonPropertyName("participant")]
    public string Participant { get; set; } = "";
    [JsonPropertyName("time")]
    public int Time { get; set; }
    [JsonPropertyName("number")]
    public int Number { get; set; }
    [JsonPropertyName("timeStr")]
    public string TimeStr { get; set; } = "";

#if !JSON_MISSING_PROPERTIES_EXCEPTION
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtensionData { get; set; }
#endif
}
