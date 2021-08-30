
using System.Text.Json.Serialization;

namespace LemonsTiming24.Server.Model.RawTiming;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "json DTO")]
public class RaceRaw
{
    [JsonPropertyName("params")]
    public Params paramaters { get; set; } = null!;

    public Progressflagstate[] progressFlagState { get; set; } = null!;
    public Entry[] entries { get; set; } = null!;
    public Bestsector[] bestSectors { get; set; } = null!;
}

[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "json DTO")]
public class Params
{
    public float remaining { get; set; }
    public float percentProgressLive { get; set; }
    public int sessionType { get; set; }
    public int svg { get; set; }
    public string eventName { get; set; } = "";
    public int sessionId { get; set; }
    public string sessionName { get; set; } = "";
    public int timestamp { get; set; }
    public string safetyCar { get; set; } = "";
    public float airTemp { get; set; }
    public float trackTemp { get; set; }
    public float humidity { get; set; }
    public float pressure { get; set; }
    public float windSpeed { get; set; }
    public int windDirection { get; set; }
    public float elapsedTime { get; set; }
    public string? raceState { get; set; }
    public long startTime { get; set; }
    public int duration { get; set; }
    public int stopTime { get; set; }
    public bool replay { get; set; }
}

[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "json DTO")]
public class Progressflagstate
{
    public float percentProgress { get; set; }
    public long startTime { get; set; }
    public bool current { get; set; }
    public string state { get; set; } = "";
}

[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "json DTO")]
public class Entry
{
    public int categoryPosition { get; set; }
    public int previousLastLap { get; set; }
    public int ranking { get; set; }
    public int positionChange { get; set; }
    public int categoryRanking { get; set; }
    public int categoryPositionChange { get; set; }
    public string number { get; set; } = "";
    public int id { get; set; }
    public string team { get; set; } = "";
    public string tyre { get; set; } = "";
    public string driver { get; set; } = "";
    public string car { get; set; } = "";
    public int lap { get; set; }
    public string gap { get; set; } = "";
    public string gapPrev { get; set; } = "";
    public string classGap { get; set; } = "";
    public string classGapPrev { get; set; } = "";
    public string lastlap { get; set; } = "";
    public int lastlapTime { get; set; }
    public int bestlapTime { get; set; }
    public int pitstop { get; set; }
    public string bestLap { get; set; } = "";
    public string speed { get; set; } = "";
    public int bestTimeSector1 { get; set; }
    public int bestTimeSector2 { get; set; }
    public int bestTimeSector3 { get; set; }
    public string bestSector1 { get; set; } = "";
    public string bestSector2 { get; set; } = "";
    public string bestSector3 { get; set; } = "";
    public string currentSector1 { get; set; } = "";
    public string currentSector2 { get; set; } = "";
    public string currentSector3 { get; set; } = "";
    public int sector { get; set; }
    public int driverId { get; set; }
    public int categoryId { get; set; }
    public string state { get; set; } = "";
    public string category { get; set; } = "";
    public string nationality { get; set; } = "";
    public string avLaps { get; set; } = "";
    public string averageTime { get; set; } = "";
    public string time { get; set; } = "";
    public string d1L1 { get; set; } = "";
    public string d2L1 { get; set; } = "";
    public int lastPassingTime { get; set; }
    public Driver[] drivers { get; set; } = null!;
    public string lastLapBestType { get; set; } = "";
    public long positionChangeTime { get; set; }
}

[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "json DTO")]
public class Driver
{
    public string ECMCountryId { get; set; } = "";
    public string ECMDriverId { get; set; } = "";
    public string country { get; set; } = "";
    public int dbId { get; set; }
    public int driverId { get; set; }
    public string firstName { get; set; } = "";
    public string lastName { get; set; } = "";
    public string license { get; set; } = "";
    public int number { get; set; }
    public string shortName { get; set; } = "";
}

[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "json DTO")]
public class Bestsector
{
    public string participant { get; set; } = "";
    public int time { get; set; }
    public int number { get; set; }
    public string timeStr { get; set; } = "";
    public int driver { get; set; }
}

