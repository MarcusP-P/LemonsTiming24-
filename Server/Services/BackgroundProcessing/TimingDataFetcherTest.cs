
using LemonsTiming24.Server.Infrastructure;
using LemonsTiming24.Server.Model.RawTiming;
using Microsoft.Extensions.Options;
using System.Reflection.Metadata;
using System.Text.Json;

namespace LemonsTiming24.Server.Services.BackgroundProcessing;
public class TimingDataFetcherTest : ITimingDataFetcher
{
    private readonly IOptions<TimingConfiguration> timingConfiguration;

    public TimingDataFetcherTest(IOptions<TimingConfiguration> timingConfiguration)
    {
        this.timingConfiguration = timingConfiguration;
    }

    public async Task DoWork(CancellationToken cancellationToken)
    {
        var allfiles = Directory.GetFiles(this.timingConfiguration.Value.SavedMessagesPath ?? "", "race-*.json", SearchOption.TopDirectoryOnly);

        Array.Sort(allfiles);

        foreach (var file in allfiles)
        {
            var fileValue = await File.ReadAllTextAsync(file, cancellationToken);



            var foo = JsonDocument.Parse(fileValue);

            System.Diagnostics.Debug.Print($"Extracting {file}");
            foreach (var foo2 in foo.RootElement.EnumerateArray())
            {
                if (foo2.ValueKind == JsonValueKind.Object)
                {
                    var foo3 = JsonSerializer.Deserialize<RaceRaw>(foo2.GetRawText());
                }
            }
        }
    }
}
