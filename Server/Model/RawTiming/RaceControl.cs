using System.Text.Json;
using System.Text.Json.Serialization;

namespace LemonsTiming24.Server.Model.RawTiming;

public class RaceControl
{
    [JsonPropertyName("backgroundColor")]
    public string BackgroundColor { get; set; } = "";

    [JsonPropertyName("blink")]
    public bool Blink { get; set; }

    [JsonPropertyName("blinkTime")]
    public int BlinkTime { get; set; }

    [JsonPropertyName("dayTime")]
    public long DayTime { get; set; }

    [JsonPropertyName("foregroundColor")]
    public string ForegroundColor { get; set; } = "";

    [JsonPropertyName("groupText")]
    public string GroupText { get; set; } = "";

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("isNull")]
    public bool IsNull { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; } = "";

    [JsonPropertyName("localTime")]
    public string LocalTime { get; set; } = "";

#if !JSON_MISSING_PROPERTIES_EXCEPTION
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtensionData { get; set; }
#endif
}
