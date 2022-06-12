
using LemonsTiming24.Server.Infrastructure;
using LemonsTiming24.Server.Model.RawTiming;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace LemonsTiming24.Server.Services.BackgroundProcessing;
public class TimingDataFetcherTest : ITimingDataFetcher
{
    private readonly IOptions<TimingConfiguration> timingConfiguration;

    public TimingDataFetcherTest(IOptions<TimingConfiguration> timingConfiguration)
    {
        ArgumentNullException.ThrowIfNull(timingConfiguration, nameof(timingConfiguration));

        this.timingConfiguration = timingConfiguration;
    }

    public async Task DoWork(CancellationToken cancellationToken)
    {
        var directoryInfo = new DirectoryInfo(this.timingConfiguration.Value?.SavedMessagesPath ?? "");
        var fileList = directoryInfo.GetFiles()
            .Where(x => x.Name.StartsWith("race-", StringComparison.InvariantCulture)
                || x.Name.StartsWith("entries-", StringComparison.InvariantCulture))
            .OrderBy(x => x.CreationTime)
            .ToArray();
        foreach (var file in fileList)
        {
            var fileValue = await File.ReadAllTextAsync(file.FullName, cancellationToken);

            var foo = JsonDocument.Parse(fileValue);

            System.Diagnostics.Debug.Print($"Extracting {file}");
            if (file.Name.StartsWith("race-", StringComparison.InvariantCulture) )
            {
                foreach (var foo2 in foo.RootElement.EnumerateArray())
                {
                    if (foo2.ValueKind == JsonValueKind.Object)
                    {
                        var foo3 = JsonSerializer.Deserialize<Race>(foo2.GetRawText());
                    }
                }
            }

            else if (file.Name.StartsWith("entries-", StringComparison.InvariantCulture))
            {
                foreach (var foo2 in foo.RootElement.EnumerateArray())
                {
                    if (foo2.ValueKind == JsonValueKind.Array)
                    {
                        var foo3 = JsonSerializer.Deserialize<Entry[]>(foo2.GetRawText());
                    }
                }
            }
        }
    }
}
