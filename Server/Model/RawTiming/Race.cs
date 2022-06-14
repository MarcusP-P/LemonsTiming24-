
using System.Text.Json.Serialization;

namespace LemonsTiming24.Server.Model.RawTiming;

public class Race
{
    [JsonPropertyName("params")]
    public Paramaters? Paramaters { get; set; }
    [JsonPropertyName("progressFlagState")]
    public Progressflagstate[]? ProgressFlagState { get; set; }
    [JsonPropertyName("entries")]
    public Entry[]? Entries { get; set; }
    [JsonPropertyName("bestSectors")]
    public Bestsector[]? BestSectors { get; set; }
    [JsonPropertyName("bestTimesByCategory")]
    public object[]? BestTimesByCategory { get; set; }
}

public class Progressflagstate
{
    [JsonPropertyName("percentProgress")]
    public float? PercentProgress { get; set; }
    [JsonPropertyName("startTime")]
    public long StartTime { get; set; }
    [JsonPropertyName("current")]
    public bool Current { get; set; }
    [JsonPropertyName("state")]
    public string State { get; set; } = "";
}

public class Bestsector
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
}
