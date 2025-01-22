
using LemonsTiming24.Server.Infrastructure.Configuration;
using LemonsTiming24.Server.Model.RawTiming;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace LemonsTiming24.Server.Services.BackgroundProcessing;
public partial class TimingDataFetcherTest : ITimingDataFetcher
{
    private readonly IOptions<TimingConfiguration> timingConfiguration;

    private readonly JsonSerializerOptions jsonSerializerOptions = new() { UnmappedMemberHandling = System.Text.Json.Serialization.JsonUnmappedMemberHandling.Disallow };
    private readonly JsonSerializerOptions jsonDesrializerOptions = new() { UnmappedMemberHandling = System.Text.Json.Serialization.JsonUnmappedMemberHandling.Disallow };

    [GeneratedRegex("(?<dataType>[a-zA-Z_]+)-(?<date>[0-9]{4}-[0-9]{2}-[0-9]{2}T)(?<hour>[0-9]{2})-(?<minute>[0-9]{2})-(?<second>[0-9]{2}\\.[0-9]*)(?<timeZone>Z)\\.json", RegexOptions.CultureInvariant)]
    private static partial Regex matchPatternGeneratedRegex();

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

            var badFileList = directoryInfo.GetFiles()
                .Where(x => !matchPatternGeneratedRegex().IsMatch(x.Name))
                .ToList();

#if JSON_MISSING_PROPERTIES_BREAK
            if (badFileList.Count != 0)
            {
                System.Diagnostics.Debugger.Break();
            }
#endif

            var fileList = directoryInfo.GetFiles()
                .Where(x => matchPatternGeneratedRegex().IsMatch(x.Name))
                .Select(x =>
                {
                    var match = matchPatternGeneratedRegex().Match(x.Name);
                    return new
                    {
                        FileName = x.Name,
                        Path = x.FullName,
                        DataType = match.Groups["dataType"].Value,
                        DateStamp = DateTime.Parse($"{match.Groups["date"].Value}{match.Groups["hour"].Value}:{match.Groups["minute"].Value}:{match.Groups["second"].Value}{match.Groups["timeZone"].Value}", null, DateTimeStyles.RoundtripKind)
                    };
                })
                .OrderBy(x => x.DateStamp)
                .ToList();

            var total = fileList.Count;
            var current = 1;
            foreach (var file in fileList)
            {
                var fileValue = await File.ReadAllTextAsync(file.Path, cancellationToken).ConfigureAwait(false);

                using var foo = JsonDocument.Parse(fileValue);

                System.Diagnostics.Debug.Print($"Extracting from {testPath}: {current}/{total} {(float)current / total:P3} {file.DateStamp:O} {file.DataType} {file.FileName}");
                switch (file.DataType)
                {
                    case "race":
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

                        break;
                    }

                    case "best_sectors":
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

                        break;
                    }

                    case "entries":
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

                        break;
                    }

                    case "stints":
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

                        break;
                    }

                    case "laps":
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

                        break;
                    }

                    case "flags":
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

                        break;
                    }

                    case "params"
                        or "race_light":
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

                        break;
                    }

                    case "race_control":
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

                        break;
                    }

                    case "clear_entries":
                        break;

                    case "aaaInternalDiagnosticsEvent":
                        break;

                    default:
#if JSON_MISSING_PROPERTIES_BREAK
                        System.Diagnostics.Debugger.Break();
#elif JSON_MISSING_PROPERTIES_EXCEPTION
                throw new Exception($"Unknown file type for {dataType}");

#endif
                        break;
                }

                current++;
            }
        }
    }
}
