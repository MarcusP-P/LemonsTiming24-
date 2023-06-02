using System.Text.Json;
using System.Text.Json.Serialization;

namespace LemonsTiming24.Server.Model.RawTiming;

public class Laps
{
    [JsonPropertyName("laps")]
    public Dictionary<string, Lap>? CarLaps { get; set; }
    [JsonPropertyName("participant")]
    public int Participant { get; set; }

#if !JSON_MISSING_PROPERTIES_EXCEPTION
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtensionData { get; set; }
#endif
}

public class Lap
{
    [JsonPropertyName("driver")]
    public int Driver { get; set; }
    [JsonPropertyName("driverLapNum")]
    public int DriverLapNum { get; set; }
    [JsonPropertyName("isValid")]
    public bool IsValid { get; set; }
    [JsonPropertyName("lapNum")]
    public int LapNum { get; set; }
    [JsonPropertyName("loopSectors")]
    public Dictionary<string, LoopSector>? LoopSectors { get; set; }
    [JsonPropertyName("pitOut")]
    public PitEvent? PitOut { get; set; }
    [JsonPropertyName("pitIn")]
    public PitEvent? PitIn { get; set; }
    [JsonPropertyName("position")]
    public int Position { get; set; }
    [JsonPropertyName("sections")]
    public Dictionary<string, Section>? Sections { get; set; }
    [JsonPropertyName("sectors")]
    public Dictionary<string, Sector>? Sectors { get; set; }
    [JsonPropertyName("startTime")]
    public long StartTime { get; set; }
    [JsonPropertyName("time")]
    public int Time { get; set; }
    [JsonPropertyName("topSpeed")]
    public float TopSpeed { get; set; }
    [JsonPropertyName("transponderCode")]
    public string TransponderCode { get; set; } = "";
    [JsonPropertyName("transponderIndex")]
    public int TransponderIndex { get; set; }

#if !JSON_MISSING_PROPERTIES_EXCEPTION
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtensionData { get; set; }
#endif
}

public class LoopSector
{
    [JsonPropertyName("dayTime")]
    public long DayTime { get; set; }
    [JsonPropertyName("number")]
    public int Number { get; set; }
    [JsonPropertyName("time")]
    public int Time { get; set; }

#if !JSON_MISSING_PROPERTIES_EXCEPTION
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtensionData { get; set; }
#endif
}

public class Section
{
    [JsonPropertyName("dayTime")]
    public long DayTime { get; set; }
    [JsonPropertyName("isValid")]
    public bool IsValid { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";
    [JsonPropertyName("speed")]
    public float Speed { get; set; }
    [JsonPropertyName("time")]
    public int Time { get; set; }

#if !JSON_MISSING_PROPERTIES_EXCEPTION
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtensionData { get; set; }
#endif
}

public class PitEvent
{
    [JsonPropertyName("driver")]
    public int Driver { get; set; }
    [JsonPropertyName("time")]
    public long Time { get; set; }

#if !JSON_MISSING_PROPERTIES_EXCEPTION
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtensionData { get; set; }
#endif
}

public class Sector
{
    [JsonPropertyName("flag")]
    public string Flag { get; set; } = "";
    [JsonPropertyName("isValid")]
    public bool IsValid { get; set; }
    [JsonPropertyName("number")]
    public int Number { get; set; }
    [JsonPropertyName("time")]
    public int Time { get; set; }

#if !JSON_MISSING_PROPERTIES_EXCEPTION
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtensionData { get; set; }
#endif
}

