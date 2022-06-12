using Microsoft.Extensions.Options;
using LemonsTiming24.Server.Infrastructure;
using SocketIOClient;
using System.Text.Json;

namespace LemonsTiming24.Server.Services.BackgroundProcessing;
public class TimingDataFetcher : ITimingDataFetcher, IDisposable
{
    private readonly IOptions<TimingConfiguration> timingConfiguration;
    private SocketIO? client;
    private bool disposedValue;

    public TimingDataFetcher(IOptions<TimingConfiguration> timingConfiguration)
    {
        this.timingConfiguration = timingConfiguration;
    }

    public async Task DoWork(CancellationToken cancellationToken)
    {
        try
        {
            this.client = new SocketIO(this.timingConfiguration.Value.BaseUrl, new SocketIOOptions
            {
                AutoUpgrade = false,
                EIO = 3,
            });

            System.Diagnostics.Debug.Print("Foo");

            this.client.OnAny(async (eventName, response) =>
                await this.WriteJsonDocument(eventName, response.ToString()));

            this.client.OnConnected += this.Client_OnConnected;
            this.client.OnReconnected += this.Client_OnReconnected;
            this.client.OnError += this.Client_OnError;
            this.client.OnDisconnected += this.Client_OnDisconnected;

            await this.client.ConnectAsync();

            System.Diagnostics.Debug.Print("Foo3");

            while (!cancellationToken.IsCancellationRequested)
            {

            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Print($"Exception: {ex.Message}");
        }
    }

    private async Task WriteJsonDocument(string name, string document)
    {
        name = string.Join("", name.Split(Path.GetInvalidFileNameChars()));
        var extension = "txt";
        var handleTime = DateTime.UtcNow;
        try
        {
            var jsonDocument = JsonDocument.Parse(document);

            try
            {
                extension = "json";
            }
            catch
            {
            }

            document = JsonSerializer.Serialize(jsonDocument, new JsonSerializerOptions { WriteIndented = true });

        }
        catch
        {
        }

        var fileName = $"{name}-{handleTime:o}.{extension}".Replace(":", "-");

        var path = Path.Join(this.timingConfiguration.Value.SavedMessagesPath, fileName);

        await File.WriteAllTextAsync(path, document);
    }

    private void Client_OnDisconnected(object? sender, string e)
    {
        System.Diagnostics.Debug.Print($"##### Disconnect");
    }

    private void Client_OnError(object? sender, string e)
    {
        System.Diagnostics.Debug.Print($"##### Error");
    }

    private void Client_OnReconnected(object? sender, int e)
    {
        System.Diagnostics.Debug.Print($"##### Re Connected");
    }

    private void Client_OnConnected(object? sender, EventArgs e)
    {
        System.Diagnostics.Debug.Print($"Connected #######");
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~TimingDataFetcher()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }

            this.client?.Dispose();

            this.disposedValue = true;
        }
    }
}
