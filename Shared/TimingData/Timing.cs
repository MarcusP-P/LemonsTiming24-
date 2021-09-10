
namespace LemonsTiming24.SharedCode.TimingData;
public class Timing
{
    /// <summary>
    /// An array of timing information.
    /// </summary>
    public TimingRow[] TimingRows { get; set; } = Array.Empty<TimingRow>();

    /// <summary>
    /// The list of cars entered.
    /// </summary>
    public List<Car> Cars { get; set; } = new List<Car>();
}
