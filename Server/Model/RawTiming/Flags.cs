using System.Text.Json;
using System.Text.Json.Serialization;

namespace LemonsTiming24.Server.Model.RawTiming;

public class Flags
{
    [JsonPropertyName("percentProgress")]
    public float PercentProgress { get; set; }
    [JsonPropertyName("startTime")]
    public long StartTime { get; set; }
    [JsonPropertyName("current")]
    public bool Current { get; set; }
    [JsonPropertyName("state")]
    public string State { get; set; } = "";

#if !JSON_MISSING_PROPERTIES_EXCEPTION
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtensionData { get; set; }
#endif
}
