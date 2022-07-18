using System.Text.Json;
using System.Text.Json.Serialization;

namespace LemonsTiming24.Server.Model.RawTiming;

public class Stints
{
    [JsonPropertyName("stints")]
    public Dictionary<string, Stint>? IndividualStints { get; set; }
    [JsonPropertyName("pitTime")]
    public int PitTime { get; set; }
    [JsonPropertyName("driveTime")]
    public int DriveTime { get; set; }
    [JsonPropertyName("participant")]
    public int Participant { get; set; }

    [JsonExtensionData]
    public Dictionary<string, object>? ExtensionData { get; set; }
}

public class Stint
{
    [JsonPropertyName("closeLapNumber")]
    public int? CloseLapNumber { get; set; }
    [JsonPropertyName("driver")]
    public int Driver { get; set; }
    [JsonPropertyName("driverAccumTime")]
    public int DriverAccumTime { get; set; }
    [JsonPropertyName("finishTime")]
    public long FinishTime { get; set; }
    [JsonPropertyName("openLapNumber")]
    public int OpenLapNumber { get; set; }
    [JsonPropertyName("startTime")]
    public long StartTime { get; set; }
    [JsonPropertyName("type")]
    public string Type { get; set; } = "";
    [JsonPropertyName("duration")]
    public int Duration { get; set; }

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtensionData { get; set; }
}
