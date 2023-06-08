using LemonsTiming24.Server.Infrastructure.JsonConverters;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LemonsTiming24.Server.Model.RawTiming;

public class Entry
{
    [JsonPropertyName("categoryPosition")]
    public int CategoryPosition { get; set; }
    [JsonPropertyName("previousLastLap")]
    public int PreviousLastLap { get; set; }
    [JsonPropertyName("ranking")]
    public int Ranking { get; set; }
    [JsonPropertyName("positionChange")]
    public int PositionChange { get; set; }
    [JsonPropertyName("categoryRanking")]
    public int CategoryRanking { get; set; }
    [JsonPropertyName("categoryPositionChange")]
    public int CategoryPositionChange { get; set; }
    [JsonPropertyName("number")]
    public string Number { get; set; } = "";
    [JsonPropertyName("id")]
    public int? Id { get; set; }
    [JsonPropertyName("team")]
    public string Team { get; set; } = "";
    [JsonPropertyName("tyre")]
    public string Tyre { get; set; } = "";
    [JsonPropertyName("driver")]
    public string Driver { get; set; } = "";
    [JsonPropertyName("bestLapDriver")]
    public string BestLapDriver { get; set; } = "";
    [JsonPropertyName("car")]
    public string Car { get; set; } = "";
    [JsonPropertyName("lap")]
    public int Lap { get; set; }
    [JsonPropertyName("gap")]
    public string Gap { get; set; } = "";
    [JsonPropertyName("gapPrev")]
    public string GapPrev { get; set; } = "";
    [JsonPropertyName("gapTime")]
    public int GapTime { get; set; }
    [JsonPropertyName("gapPrevTime")]
    public int GapPrevTime { get; set; }
    [JsonPropertyName("gapLaps")]
    public string GapLaps { get; set; } = "";
    [JsonPropertyName("gapPrevLaps")]
    public string GapPrevLaps { get; set; } = "";
    [JsonPropertyName("classGap")]
    public string ClassGap { get; set; } = "";
    [JsonPropertyName("classGapPrev")]
    public string ClassGapPrev { get; set; } = "";
    [JsonPropertyName("classGapTime")]
    public int ClassGapTime { get; set; }
    [JsonPropertyName("classGapPrevTime")]
    public int ClassGapPrevTime { get; set; }
    [JsonPropertyName("classGapLaps")]
    public string ClassGapLaps { get; set; } = "";
    [JsonPropertyName("classGapPrevLaps")]
    public string ClassGapPrevLaps { get; set; } = "";
    [JsonPropertyName("lastlap")]
    public string LastLap { get; set; } = "";
    [JsonPropertyName("lastlapTime")]
    public int LastLapTime { get; set; }
    [JsonPropertyName("bestlapTime")]
    public int BestLapTime { get; set; }
    [JsonPropertyName("secondBestlapTime")]
    public int SecondBestLapTime { get; set; }
    [JsonPropertyName("pitstop")]
    public int Pitstop { get; set; }
    [JsonPropertyName("bestLap")]
    public string BestLap { get; set; } = "";
    [JsonPropertyName("secondBestLap")]
    public string SecondBestLap { get; set; } = "";
    [JsonPropertyName("speed")]
    public string Speed { get; set; } = "";
    [JsonPropertyName("bestTimeSector1")]
    public int BestTimeSector1 { get; set; }
    [JsonPropertyName("bestTimeSector2")]
    public int BestTimeSector2 { get; set; }
    [JsonPropertyName("bestTimeSector3")]
    public int BestTimeSector3 { get; set; }
    [JsonPropertyName("bestSector1")]
    // in some situations, this is 0 rather than null
    [JsonConverter(typeof(ZeroMeansNullToStringJsonConverter))]
    public string BestSector1 { get; set; } = "";
    [JsonPropertyName("bestSector2")]
    // in some situations, this is 0 rather than null
    [JsonConverter(typeof(ZeroMeansNullToStringJsonConverter))]
    public string BestSector2 { get; set; } = "";
    [JsonPropertyName("bestSector3")]
    // in some situations, this is 0 rather than null
    [JsonConverter(typeof(ZeroMeansNullToStringJsonConverter))]
    public string BestSector3 { get; set; } = "";
    [JsonPropertyName("currentSector1")]
    // in some situations, this is 0 rather than null
    [JsonConverter(typeof(ZeroMeansNullToStringJsonConverter))]
    public string? CurrentSector1 { get; set; }
    [JsonPropertyName("currentSector2")]
    // in some situations, this is 0 rather than null
    [JsonConverter(typeof(ZeroMeansNullToStringJsonConverter))]
    public string? CurrentSector2 { get; set; }
    [JsonPropertyName("currentSector3")]
    // in some situations, this is 0 rather than null
    [JsonConverter(typeof(ZeroMeansNullToStringJsonConverter))]
    public string? CurrentSector3 { get; set; }
    [JsonPropertyName("sector")]
    public int Sector { get; set; }
    [JsonPropertyName("driverId")]
    public int DriverId { get; set; }
    [JsonPropertyName("categoryId")]
    public int? CategoryId { get; set; }
    [JsonPropertyName("state")]
    public string State { get; set; } = "";
    [JsonPropertyName("category")]
    [JsonConverter(typeof(NumericOrStringToStringJsonConverter))]
    public string Category { get; set; } = "";
    [JsonPropertyName("nationality")]
    public string Nationality { get; set; } = "";
    [JsonPropertyName("avLaps")]
    public string AvLaps { get; set; } = "";
    [JsonPropertyName("averageTime")]
    public string AverageTime { get; set; } = "";
    [JsonPropertyName("time")]
    public string Time { get; set; } = "";
    [JsonPropertyName("d1L1")]
    public string D1L1 { get; set; } = "";
    [JsonPropertyName("d2L1")]
    public string D2L1 { get; set; } = "";
    [JsonPropertyName("lastPassingTime")]
    public int LastPassingTime { get; set; }
    [JsonPropertyName("drivers")]
    public Driver[]? Drivers { get; set; }
    [JsonPropertyName("lastLapBestType")]
    public string LastLapBestType { get; set; } = "";
    [JsonPropertyName("positionChangeTime")]
    public long PositionChangeTime { get; set; }
    [JsonPropertyName("categoryPositionChangeTime")]
    public long CategoryPositionChangeTime { get; set; }

    // No longer used?
    [JsonPropertyName("lastLapDiff")]
    public string LastLapDiff { get; set; } = "";
    [JsonPropertyName("position")]
    public string Position { get; set; } = "";
    [JsonPropertyName("isWEC")]
    public string IsWec { get; set; } = "";

#if !JSON_MISSING_PROPERTIES_EXCEPTION
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtensionData { get; set; }
#endif
}

public class Driver
{
    [JsonPropertyName("ECM Country Id")]
    public string ECMCountryId { get; set; } = "";
    [JsonPropertyName("ECM Driver Id")]
    public string ECMDriverId { get; set; } = "";
    [JsonPropertyName("country")]
    public string Country { get; set; } = "";
    [JsonPropertyName("dbId")]
    public int DbId { get; set; }
    [JsonPropertyName("driverId")]
    public int DriverId { get; set; }
    [JsonPropertyName("firstName")]
    public string FirstName { get; set; } = "";
    [JsonPropertyName("lastName")]
    public string LastName { get; set; } = "";
    [JsonPropertyName("license")]
    public string License { get; set; } = "";
    [JsonPropertyName("number")]
    public int Number { get; set; }
    [JsonPropertyName("shortName")]
    public string ShortName { get; set; } = "";
    [JsonPropertyName("*Extra 4")]
    public string Extra4 { get; set; } = "";
    [JsonPropertyName("*Extra 5")]
    public string Extra5 { get; set; } = "";

#if !JSON_MISSING_PROPERTIES_EXCEPTION
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtensionData { get; set; }
#endif
}
