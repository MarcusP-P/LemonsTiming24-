
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LemonsTiming24.Server.Model.RawTiming;

public class Race
{
    [JsonPropertyName("params")]
    public Paramaters? Paramaters { get; set; }
    [JsonPropertyName("progressFlagState")]
    public ProgressFlagState[]? ProgressFlagState { get; set; }
    [JsonPropertyName("entries")]
    public Entry[]? Entries { get; set; }
    [JsonPropertyName("bestSectors")]
    public BestSector[]? BestSectors { get; set; }
    [JsonPropertyName("bestTimesByCategory")]
    public BestTimesByCategory[]? BestTimesByCategory { get; set; }

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtensionData { get; set; }
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

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtensionData { get; set; }
}

public class BestTimesByCategory
{

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtensionData { get; set; }
}
