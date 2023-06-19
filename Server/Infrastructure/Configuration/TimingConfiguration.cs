using System.Text.Json;

namespace LemonsTiming24.Server.Infrastructure.Configuration;

public class TimingConfiguration
{
    public Uri? BaseUrl { get; set; }

    public string? SocketPath { get; set; }

    public string? ArchiveMessagesPath { get; set; }

    public string? ArchiveListPath { get; set; }

    private ICollection<string>? archivedMessagesPathsInternal;

    //    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "This is a DTO.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "This is a DTO.")]
    public ICollection<string>? ArchivedMessagesPaths
    {
        get
        {
            if (this.archivedMessagesPathsInternal is null)
            {
                if (this.ArchiveListPath is null)
                {
                    return null;
                }

                this.archivedMessagesPathsInternal = new List<string>();

                using var openStream = File.OpenRead(this.ArchiveListPath);

                var sourceLocations = JsonSerializer.Deserialize<SourceLocations>(openStream);

                if (sourceLocations is null || sourceLocations.Folders is null)
                {
                    return this.archivedMessagesPathsInternal;
                }

                var path = Path.GetDirectoryName(this.ArchiveListPath);

                foreach (var folder in sourceLocations.Folders)
                {
                    this.archivedMessagesPathsInternal.Add(Path.Join(path, folder.Path));
                }
            }

            return this.archivedMessagesPathsInternal;
        }

        set => this.archivedMessagesPathsInternal = value;
    }
}
