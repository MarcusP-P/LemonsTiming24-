namespace LemonsTiming24.Server.Model.Database.RawData;

public class RawTimingSession
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required Uri SourceUrl { get; set; }
    public required ICollection<RawJsonResponse> RawJsonResponses { get; set; }
}
