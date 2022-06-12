
using System.Text.Json.Serialization;

namespace LemonsTiming24.Server.Model.RawTiming;

public class Race
{
    [JsonPropertyName("params")]
    public Params? Paramaters { get; set; }
    [JsonPropertyName("progressFlagState")]
    public Progressflagstate[]? ProgressFlagState { get; set; }
    [JsonPropertyName("entries")]
    public Entry[]? Entries { get; set; }
    [JsonPropertyName("bestSectors")]
    public Bestsector[]? BestSectors { get; set; }
    [JsonPropertyName("bestTimesByCategory")]
    public object[]? BestTimesByCategory { get; set; }
}

public class Params
{
    [JsonPropertyName("remaining")]
    public float Remaining { get; set; }
    [JsonPropertyName("percentProgressLive")]
    public float PercentProgressLive { get; set; }
    [JsonPropertyName("sessionType")]
    public int SessionType { get; set; }
    [JsonPropertyName("svg")]
    public int Svg { get; set; }
    [JsonPropertyName("eventName")]
    public string EventName { get; set; } = "";
    [JsonPropertyName("sessionId")]
    public int SessionId { get; set; }
    [JsonPropertyName("sessionName")]
    public string SessionName { get; set; } = "";
    [JsonPropertyName("timestamp")]
    public int Timestamp { get; set; }
    [JsonPropertyName("safetyCar")]
    public string SafetyCar { get; set; } = "";
    [JsonPropertyName("airTemp")]
    public float AirTemp { get; set; }
    [JsonPropertyName("trackTemp")]
    public float TrackTemp { get; set; }
    [JsonPropertyName("humidity")]
    public int Humidity { get; set; }
    [JsonPropertyName("pressure")]
    public float Pressure { get; set; }
    [JsonPropertyName("windSpeed")]
    public float WindSpeed { get; set; }
    [JsonPropertyName("windDirection")]
    public int WindDirection { get; set; }
    [JsonPropertyName("elapsedTime")]
    public float ElapsedTime { get; set; }
    [JsonPropertyName("raceState")]
    public string RaceState { get; set; } = "";
    [JsonPropertyName("startTime")]
    public long StartTime { get; set; }
    [JsonPropertyName("duration")]
    public int Duration { get; set; }
    [JsonPropertyName("stopTime")]
    public int StopTime { get; set; }
    [JsonPropertyName("stoppedSinceTime")]
    public int StoppedSinceTime { get; set; }
    [JsonPropertyName("replay")]
    public bool Replay { get; set; }
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
