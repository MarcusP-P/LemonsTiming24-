
using LemonsTiming24.Server.Infrastructure.Configuration;
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
        foreach (var testPath in this.timingConfiguration.Value?.ArchivedMessagesPaths ?? Array.Empty<string>())
        {
            var directoryInfo = new DirectoryInfo(testPath);

            var fileList = directoryInfo.GetFiles()
                .Where(x =>
                    x.Name.StartsWith("best_sectors-", StringComparison.InvariantCulture)
                    || x.Name.StartsWith("entries-", StringComparison.InvariantCulture)
                    || x.Name.StartsWith("flags-", StringComparison.InvariantCulture)
                    || x.Name.StartsWith("laps-", StringComparison.InvariantCulture)
                    || x.Name.StartsWith("params-", StringComparison.InvariantCulture)
                    || x.Name.StartsWith("race-", StringComparison.InvariantCulture)
                    || x.Name.StartsWith("race_control-", StringComparison.InvariantCulture)
                    || x.Name.StartsWith("race_light-", StringComparison.InvariantCulture)
                    || x.Name.StartsWith("stints-", StringComparison.InvariantCulture)
                    )
                .OrderBy(x => x.Name.Remove(0, x.Name.IndexOf("-", StringComparison.InvariantCulture) + 1))
                .ToList();

            var total = fileList.Count;
            var current = 1;
            foreach (var file in fileList)
            {
                var fileValue = await File.ReadAllTextAsync(file.FullName, cancellationToken).ConfigureAwait(false);

                using var foo = JsonDocument.Parse(fileValue);

                System.Diagnostics.Debug.Print($"Extracting from {testPath}: {current}/{total} {(float)current / total:P3} {file.Name.Remove(0, file.Name.IndexOf("-", StringComparison.InvariantCulture) + 1)} {file.Name}");
                if (file.Name.StartsWith("race-", StringComparison.InvariantCulture))
                {
                    foreach (var foo2 in foo.RootElement.EnumerateArray())
                    {
                        if (foo2.ValueKind == JsonValueKind.Object)
                        {
                            var foo3 = JsonSerializer.Deserialize<Race>(foo2.GetRawText(), new JsonSerializerOptions { UnmappedMemberHandling = System.Text.Json.Serialization.JsonUnmappedMemberHandling.Disallow });

#if JSON_MISSING_PROPERTIES_BREAK
                            if (foo3?.ExtensionData != null)
                            {
                                System.Diagnostics.Debugger.Break();
                            }

                            if (foo3?.Parameters?.ExtensionData != null)
                            {
                                System.Diagnostics.Debugger.Break();
                            }
#endif
                            if (foo3?.ProgressFlagState != null)
                            {
#if JSON_MISSING_PROPERTIES_BREAK
                                var foo4 = foo3.ProgressFlagState
                                    .Where(x => x?.ExtensionData != null)
                                    .ToList();
                                if (foo4.Count != 0)
                                {
                                    System.Diagnostics.Debugger.Break();
                                }
#endif
                            }

                            if (foo3?.Entries != null)
                            {
#if JSON_MISSING_PROPERTIES_BREAK
                                var foo4 = foo3.Entries.Where(x => x?.ExtensionData != null).ToList();
                                if (foo4.Count != 0)
                                {
                                    System.Diagnostics.Debugger.Break();
                                }

                                var foo5 = foo3.Entries.Where(x => x?.Drivers != null)
                                    .SelectMany(x => x.Drivers!)
                                    .Where(x => x.ExtensionData != null)
                                    .ToList();
                                if (foo5.Count != 0)
                                {
                                    System.Diagnostics.Debugger.Break();
                                }
#endif
                            }

                            if (foo3?.BestSectors != null)
                            {
#if JSON_MISSING_PROPERTIES_BREAK
                                var foo4 = foo3.BestSectors
                                    .Where(x => x?.ExtensionData != null)
                                    .ToList();
                                if (foo4.Count != 0)
                                {
                                    System.Diagnostics.Debugger.Break();
                                }
#endif
                            }

                            if (foo3?.BestTimesByCategory != null)
                            {
#if JSON_MISSING_PROPERTIES_BREAK
                                var foo4 = foo3.BestTimesByCategory
                                    .Where(x => x?.ExtensionData != null)
                                    .ToList();
                                if (foo4.Count != 0)
                                {
                                    System.Diagnostics.Debugger.Break();
                                }
#endif
                            }
                        }
                    }
                }

                else if (file.Name.StartsWith("best_sectors-", StringComparison.InvariantCulture))
                {
                    foreach (var foo2 in foo.RootElement.EnumerateArray())
                    {
                        if (foo2.ValueKind == JsonValueKind.Array)
                        {
                            var foo3 = JsonSerializer.Deserialize<BestSector[]>(foo2.GetRawText(), new JsonSerializerOptions { UnmappedMemberHandling = System.Text.Json.Serialization.JsonUnmappedMemberHandling.Disallow });
                            if (foo3 is not null && foo3.Length != 0)
                            {
#if JSON_MISSING_PROPERTIES_BREAK
                                var foo4 = foo3.Where(x => x?.ExtensionData != null).ToList();
                                if (foo4.Count != 0)
                                {
                                    System.Diagnostics.Debugger.Break();
                                }
#endif
                            }
                        }
                    }
                }

                else if (file.Name.StartsWith("entries-", StringComparison.InvariantCulture))
                {
                    foreach (var foo2 in foo.RootElement.EnumerateArray())
                    {
                        if (foo2.ValueKind == JsonValueKind.Array)
                        {
                            var foo3 = JsonSerializer.Deserialize<Entry[]>(foo2.GetRawText(), new JsonSerializerOptions { UnmappedMemberHandling = System.Text.Json.Serialization.JsonUnmappedMemberHandling.Disallow });
                            if (foo3 is not null && foo3.Length != 0)
                            {
#if JSON_MISSING_PROPERTIES_BREAK
                                var foo4 = foo3.Where(x => x?.ExtensionData != null).ToList();
                                if (foo4.Count != 0)
                                {
                                    System.Diagnostics.Debugger.Break();
                                }

                                var foo5 = foo3.Where(x => x?.Drivers != null)
                                    .SelectMany(x => x.Drivers!)
                                    .Where(x => x.ExtensionData != null)
                                    .ToList();
                                if (foo5.Count != 0)
                                {
                                    System.Diagnostics.Debugger.Break();
                                }
#endif
                            }
                        }
                    }
                }

                else if (file.Name.StartsWith("stints-", StringComparison.InvariantCulture))
                {
                    foreach (var foo2 in foo.RootElement.EnumerateArray())
                    {
                        if (foo2.ValueKind == JsonValueKind.Array)
                        {
                            var foo3 = JsonSerializer.Deserialize<Stints[]>(foo2.GetRawText(), new JsonSerializerOptions { UnmappedMemberHandling = System.Text.Json.Serialization.JsonUnmappedMemberHandling.Disallow });
                            if (foo3 is not null && foo3.Length != 0)
                            {
#if JSON_MISSING_PROPERTIES_BREAK
                                var foo4 = foo3.Where(x => x?.ExtensionData != null).ToList();
                                if (foo4.Count != 0)
                                {
                                    System.Diagnostics.Debugger.Break();
                                }

                                var foo5 = foo3.Where(x => x?.IndividualStints != null)
                                    .SelectMany(x => x.IndividualStints?.Values!)
                                    .Where(x => x?.ExtensionData != null)
                                    .ToList();
                                if (foo5.Count != 0)
                                {
                                    System.Diagnostics.Debugger.Break();
                                }
#endif
                            }
                        }
                    }
                }

                else if (file.Name.StartsWith("laps-", StringComparison.InvariantCulture))
                {
                    foreach (var foo2 in foo.RootElement.EnumerateArray())
                    {
                        if (foo2.ValueKind == JsonValueKind.Array)
                        {
                            var foo3 = JsonSerializer.Deserialize<Laps[]>(foo2.GetRawText(), new JsonSerializerOptions { UnmappedMemberHandling = System.Text.Json.Serialization.JsonUnmappedMemberHandling.Disallow });
                            if (foo3 is not null && foo3.Length != 0)
                            {
#if JSON_MISSING_PROPERTIES_BREAK
                                var foo4 = foo3.Where(x => x?.ExtensionData != null).ToList();
                                if (foo4.Count != 0)
                                {
                                    System.Diagnostics.Debugger.Break();
                                }

                                var foo5 = foo3.Where(x => x?.CarLaps != null)
                                    .SelectMany(x => x.CarLaps!.Values)
                                    .ToList();

                                var foo6 = foo5.Where(x => x.ExtensionData != null).ToList();
                                if (foo6.Count != 0)
                                {
                                    System.Diagnostics.Debugger.Break();
                                }

                                var foo7 = foo5.Where(x => x.LoopSectors != null)
                                    .SelectMany(x => x.LoopSectors!.Values)
                                    .Where(x => x?.ExtensionData != null)
                                    .ToList();
                                if (foo7.Count != 0)
                                {
                                    System.Diagnostics.Debugger.Break();
                                }

                                var foo8 = foo5
                                    .Where(x => x.PitOut?.ExtensionData != null)
                                    .ToList();
                                if (foo8.Count != 0)
                                {
                                    System.Diagnostics.Debugger.Break();
                                }

                                var foo9 = foo5
                                    .Where(x => x.PitIn?.ExtensionData != null)
                                    .ToList();
                                if (foo9.Count != 0)
                                {
                                    System.Diagnostics.Debugger.Break();
                                }

                                var foo10 = foo5.Where(x => x.Sections != null)
                                    .SelectMany(x => x.Sections!.Values)
                                    .Where(x => x?.ExtensionData != null)
                                    .ToList();
                                if (foo10.Count != 0)
                                {
                                    System.Diagnostics.Debugger.Break();
                                }

                                var foo11 = foo5.Where(x => x.Sectors != null)
                                    .SelectMany(x => x.Sectors!.Values)
                                    .Where(x => x?.ExtensionData != null)
                                    .ToList();
                                if (foo11.Count != 0)
                                {
                                    System.Diagnostics.Debugger.Break();
                                }
#endif
                            }
                        }
                    }
                }

                else if (file.Name.StartsWith("flags-", StringComparison.InvariantCulture))
                {
                    foreach (var foo2 in foo.RootElement.EnumerateArray())
                    {
                        if (foo2.ValueKind == JsonValueKind.Array)
                        {
                            var foo3 = JsonSerializer.Deserialize<Flags[]>(foo2.GetRawText(), new JsonSerializerOptions { UnmappedMemberHandling = System.Text.Json.Serialization.JsonUnmappedMemberHandling.Disallow });
                            if (foo3 is not null && foo3.Length != 0)
                            {
#if JSON_MISSING_PROPERTIES_BREAK
                                var foo4 = foo3.Where(x => x?.ExtensionData != null).ToList();
                                if (foo4.Count != 0)
                                {
                                    System.Diagnostics.Debugger.Break();
                                }
#endif
                            }
                        }
                    }
                }

                else if (file.Name.StartsWith("params-", StringComparison.InvariantCulture)
                    || file.Name.StartsWith("race_light-", StringComparison.InvariantCulture))
                {
                    foreach (var foo2 in foo.RootElement.EnumerateArray())
                    {
                        if (foo2.ValueKind == JsonValueKind.Object)
                        {
                            var foo3 = JsonSerializer.Deserialize<Parameters>(foo2.GetRawText(), new JsonSerializerOptions { UnmappedMemberHandling = System.Text.Json.Serialization.JsonUnmappedMemberHandling.Disallow });
#if JSON_MISSING_PROPERTIES_BREAK
                            if (foo3?.ExtensionData != null)
                            {
                                System.Diagnostics.Debugger.Break();
                            }
#endif
                        }
                    }
                }
                else if (file.Name.StartsWith("race_control-", StringComparison.InvariantCulture))
                {
                    foreach (var foo2 in foo.RootElement.EnumerateArray())
                    {
                        if (foo2.ValueKind == JsonValueKind.Array)
                        {
                            var foo3 = JsonSerializer.Deserialize<RaceControl[]>(foo2.GetRawText(), new JsonSerializerOptions { UnmappedMemberHandling = System.Text.Json.Serialization.JsonUnmappedMemberHandling.Disallow });
                            if (foo3 is not null && foo3.Length != 0)
                            {
#if JSON_MISSING_PROPERTIES_BREAK
                                var foo4 = foo3.Where(x => x?.ExtensionData != null).ToList();
                                if (foo4.Count != 0)
                                {
                                    System.Diagnostics.Debugger.Break();
                                }
#endif
                            }
                        }
                    }
                }
                else
                {
#if JSON_MISSING_PROPERTIES_BREAK
                    System.Diagnostics.Debugger.Break();
#elif JSON_MISSING_PROPERTIES_EXCEPTION
                throw new Exception($"Unknown file type for {file.Name}");

#endif
                }

                current++;
            }
        }
    }
}
