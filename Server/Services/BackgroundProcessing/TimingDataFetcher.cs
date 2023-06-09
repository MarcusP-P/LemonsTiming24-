using LemonsTiming24.Server.Infrastructure;
using LemonsTiming24.Server.Infrastructure.SocketIO;
using Microsoft.Extensions.Options;
using SocketIOClient;
using System.Text.Json;

namespace LemonsTiming24.Server.Services.BackgroundProcessing;
public class TimingDataFetcher : ITimingDataFetcher, IDisposable
{
    private readonly IOptions<TimingConfiguration> timingConfiguration;
    private SocketIO? client;
    private bool disposedValue;
    private readonly ILogger<TimingDataFetcher> logger;
    private readonly DebuggingHttpClient debuggingHttpClient;

    private DateTime clientStartTime;

    public TimingDataFetcher(IOptions<TimingConfiguration> timingConfiguration,
        ILogger<TimingDataFetcher> logger,
        DebuggingHttpClient debuggingHttpClient)
    {
        ArgumentNullException.ThrowIfNull(timingConfiguration, nameof(timingConfiguration));

        this.timingConfiguration = timingConfiguration;
        this.logger = logger;
        this.debuggingHttpClient = debuggingHttpClient;
    }

    private sealed class ConnectionStatus
    {
        public string EventName { get; set; } = "";
        public string? Reason { get; set; }
        public int? AttemptNumber { get; set; }
        public TimeSpan? Duration { get; set; }
        public string? ConnectionId { get; set; }
    }

    public async Task DoWork(CancellationToken cancellationToken)
    {
        try
        {
            // Create the folder if it doesn't exist
            if (!Directory.Exists(this.timingConfiguration.Value.ArchiveMessagesPath))
            {
                _ = Directory.CreateDirectory(this.timingConfiguration.Value.ArchiveMessagesPath ?? "");
            }

            this.client = new SocketIO(this.timingConfiguration.Value.BaseUrl, new SocketIOOptions
            {
                AutoUpgrade = false,
                EIO = EngineIO.V3,
                Transport = SocketIOClient.Transport.TransportProtocol.Polling,
                Reconnection = true,
            })
            {
                HttpClient = this.debuggingHttpClient
            };

            this.client.OnAny(async (eventName, response) =>
                await this.WriteJsonDocument(eventName, response.ToString()).ConfigureAwait(false));

            this.client.OnConnected += this.ClientOnConnected;
            this.client.OnReconnected += this.ClientOnReconnected;
            this.client.OnError += this.ClientOnError;
            this.client.OnDisconnected += this.ClientOnDisconnected;
            this.client.OnReconnectAttempt += this.ClientOnReconnectAttempt;

            this.clientStartTime = DateTime.Now;
            await this.client.ConnectAsync().ConfigureAwait(false);

            await Task.Delay(-1, cancellationToken).ConfigureAwait(false);

            /*
            // If we need to do something regularly, use this instead of the above block
            while (!cancellationToken.IsCancellationRequested)
            {

                await Task.Delay(15000, cancellationToken);

                if (DateTime.Now - this.clientStartTime > TimeSpan.FromMinutes(5))
                {
                    System.Diagnostics.Debug.Print($"Forced Reconnect after {DateTime.Now - this.clientStartTime:c}");
                    await this.client.DisconnectAsync();
                    await this.client.ConnectAsync();
                }
                
            }
            */
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Print($"Exception: {ex.Message}");
            throw;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Quick and dirty detect if it's a json file")]
    private async Task WriteJsonDocument(string name, string document)
    {
        _ = name ?? throw new ArgumentNullException(nameof(name));

        name = string.Join("", name.Split(Path.GetInvalidFileNameChars()));
        var extension = "txt";
        var handleTime = DateTime.UtcNow;
        // TODO: Tidy this up, and try to clean up CA1031
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

        var fileName = $"{name}-{handleTime:o}.{extension}".Replace(":", "-", StringComparison.InvariantCulture);

        var path = Path.Join(this.timingConfiguration.Value.ArchiveMessagesPath, fileName);

        await File.WriteAllTextAsync(path, document).ConfigureAwait(false);
    }

    private void LogEvent(string eventName, string? reason = null, string? connectionId = null, int? attemptNumber = null, DateTime? eventStart = null, DateTime? eventEnd = null)
    {
        var output = new ConnectionStatus
        {
            EventName = eventName,
            Reason = reason,
            AttemptNumber = attemptNumber,
            Duration = (eventStart is not null && eventEnd is not null) ? eventEnd - eventStart : null,
            ConnectionId = connectionId,
        };
        var document = JsonSerializer.Serialize(output);
        _ = this.WriteJsonDocument("aaaInternalDiagnosticsEvent", document);
    }

    private void ClientOnDisconnected(object? sender, string e)
    {
        var now = DateTime.Now;
        System.Diagnostics.Debug.Print($"##### Disconnect after {now - this.clientStartTime:c} with reason: {e}");
        this.LogEvent("Disconnect", connectionId: this.client!.Id, reason: e, eventStart: this.clientStartTime, eventEnd: now);
    }

    private void ClientOnError(object? sender, string e)
    {
        System.Diagnostics.Debug.Print($"##### Error: {e}");
        this.LogEvent("Error", connectionId: this.client!.Id, reason: e);
    }

    private void ClientOnReconnected(object? sender, int e)
    {
        System.Diagnostics.Debug.Print($"##### Re Connected");
        this.LogEvent("Reconnected", connectionId: this.client!.Id, attemptNumber: e);
    }

    private async void ClientOnConnected(object? sender, EventArgs e)
    {
        this.clientStartTime = DateTime.Now;
        System.Diagnostics.Debug.Print($"Connected ####### {this.clientStartTime:f}");
        this.LogEvent("Connected", connectionId: this.client!.Id);
        await this.client!.EmitAsync("stints:join").ConfigureAwait(false);
        await this.client!.EmitAsync("laps:join").ConfigureAwait(false);
        await this.client!.EmitAsync("race_control:join").ConfigureAwait(false);
    }

    private void ClientOnReconnectAttempt(object? sender, int e)
    {
        System.Diagnostics.Debug.Print($"###### Reconnect Attempt number {e} {DateTime.Now:f} #######");
        this.LogEvent("Reconnection attempt", connectionId: this.client!.Id, attemptNumber: e);
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
