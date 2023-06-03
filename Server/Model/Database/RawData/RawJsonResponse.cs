using System.Text.Json;

namespace LemonsTiming24.Server.Model.Database.RawData;

public class RawJsonResponse : IDisposable
{
    private bool disposedValue;

    public required Guid Id { get; set; }

    public required string DataName { get; set; }

    public required JsonDocument DataValue { get; set; }

    public required Session Session { get; set; }

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposedValue)
        {
            if (disposing)
            {
                this.DataValue.Dispose();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            this.disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
