using LemonsTiming24.Server.Model.Database.RawData;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Text.Json;

namespace LemonsTiming24.Server.Model.Database.Context;

public class TimingRawContext : DbContext
{
    public TimingRawContext(DbContextOptions<TimingRawContext> options)
        : base(options)
    {
    }

    public DbSet<Session> Sessions { get; set; } = null!;
    public DbSet<RawJsonResponse> RawJsonResponses { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<Session>();
        _ = modelBuilder.Entity<RawJsonResponse>()
            .Property(e => e.DataValue)
            .HasConversion(v => JsonSerializer.Serialize(v, new JsonSerializerOptions { WriteIndented = true }),
            v => JsonDocument.Parse(v, default));
    }
}
