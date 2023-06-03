
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LemonsTiming24.Server.Model.RawTiming;

public class Race
{
    [JsonPropertyName("params")]
    public Parameters? Parameters { get; set; }
    [JsonPropertyName("progressFlagState")]
    public ProgressFlagState[]? ProgressFlagState { get; set; }
    [JsonPropertyName("entries")]
    public Entry[]? Entries { get; set; }
    [JsonPropertyName("bestSectors")]
    public BestSector[]? BestSectors { get; set; }
    [JsonPropertyName("bestTimesByCategory")]
    public BestTimesByCategory[]? BestTimesByCategory { get; set; }

#if !JSON_MISSING_PROPERTIES_EXCEPTION
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtensionData { get; set; }
#endif
}

public class ProgressFlagState
{
    [JsonPropertyName("percentProgress")]
    public float? PercentProgress { get; set; }
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

public class BestTimesByCategory
{

#if !JSON_MISSING_PROPERTIES_EXCEPTION
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtensionData { get; set; }
#endif
}
