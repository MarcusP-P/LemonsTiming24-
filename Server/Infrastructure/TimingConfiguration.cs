
namespace LemonsTiming24.Server.Infrastructure;
public class TimingConfiguration
{
    public Uri? BaseUrl { get; set; }
    public string? SocketPath { get; set; }
    public string? ArchiveMessagesPath { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "This is a DTO.")]
    public string[]? ArchivedMessagesPaths { get; set; }
}
