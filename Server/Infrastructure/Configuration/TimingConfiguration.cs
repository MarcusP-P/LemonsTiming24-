
namespace LemonsTiming24.Server.Infrastructure.Configuration;
using System.Text.Json;

public class TimingConfiguration
{
    private ICollection<string>? archivedMessagesPaths;

    public Uri? BaseUrl { get; set; }

    public string? SocketPath { get; set; }

    public string? ArchiveMessagesPath { get; set; }

    public string? ArchiveListPath { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "This is a DTO.")]
    public ICollection<string>? ArchivedMessagesPaths
    {
        get
        {
            if (this.archivedMessagesPaths is null)
            {
                if (this.ArchiveListPath is null)
                {
                    return null;
                }

                this.archivedMessagesPaths = new List<string>();

                using FileStream openStream = File.OpenRead(this.ArchiveListPath);

                var sourceLocations = JsonSerializer.Deserialize<SourceLocations>(openStream);

                var path=Path.GetDirectoryName(this.ArchiveListPath);

                foreach (var folder in sourceLocations.Folders)
                {
                    this.archivedMessagesPaths.Add(Path.Join(path, folder.Path));
                }
            }

            return this.archivedMessagesPaths;
        }

        set => this.archivedMessagesPaths = value;
    }
}
