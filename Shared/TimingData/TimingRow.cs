
namespace LemonsTiming24.SharedCode.TimingData;
public class TimingRow
{
    public Car Car { get; set; } = null!;
    public int Position { get; set; }

    public string Sector1 { get; set; } = "";
    public string Sector2 { get; set; } = "";
    public string Sector3 { get; set; } = "";
    public string BestSector1 { get; set; } = "";
    public string BestSector2 { get; set; } = "";
    public string BestSector3 { get; set; } = "";
    public int BestSector1Miliseconds { get; set; }
    public int BestSector2Miliseconds { get; set; }
    public int BestSector3Miliseconds { get; set; }

    public string BestLap { get; set; } = "";
    public string LastLap { get; set; } = "";
    public int BestLapMiliseconds { get; set; }
    public int LastLapMiliseconds { get; set; }

    public int Lap { get; set; }
}
