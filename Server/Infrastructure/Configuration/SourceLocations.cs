using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace LemonsTiming24.Server.Infrastructure.Configuration;

public class SourceLocations
{
    [JsonPropertyName("folders")]
    [SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "These are all DTOs")]
    public Folder[]? Folders { get; set; }
}

public class Folder
{
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
    [JsonPropertyName("path")]
    public string Path { get; set; } = string.Empty;
}
