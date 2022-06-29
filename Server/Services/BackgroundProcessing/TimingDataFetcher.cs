﻿using Microsoft.Extensions.Options;
using LemonsTiming24.Server.Infrastructure;
using SocketIOClient;
using System.Text.Json;

namespace LemonsTiming24.Server.Services.BackgroundProcessing;
public class TimingDataFetcher : ITimingDataFetcher, IDisposable
{
    private readonly IOptions<TimingConfiguration> timingConfiguration;
    private SocketIO? client;
    private bool disposedValue;

    private DateTime clientStartTime;

    public TimingDataFetcher(IOptions<TimingConfiguration> timingConfiguration)
    {
        ArgumentNullException.ThrowIfNull(timingConfiguration, nameof(timingConfiguration));

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

            this.client.OnAny(async (eventName, response) =>
                await this.WriteJsonDocument(eventName, response.ToString()));

            this.client.OnConnected += this.Client_OnConnected;
            this.client.OnReconnected += this.Client_OnReconnected;
            this.client.OnError += this.Client_OnError;
            this.client.OnDisconnected += this.Client_OnDisconnected;
            this.client.OnReconnectAttempt += this.Client_OnReconnectAttempt;

            this.clientStartTime = DateTime.Now;
            await this.client.ConnectAsync();

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
        System.Diagnostics.Debug.Print($"##### Disconnect after {DateTime.Now - this.clientStartTime:c}: {e}");
    }

    private void Client_OnError(object? sender, string e)
    {
        System.Diagnostics.Debug.Print($"##### Error");
    }

    private void Client_OnReconnected(object? sender, int e)
    {
        System.Diagnostics.Debug.Print($"##### Re Connected");
    }

    private async void Client_OnConnected(object? sender, EventArgs e)
    {
        this.clientStartTime = DateTime.Now;
        System.Diagnostics.Debug.Print($"Connected ####### {this.clientStartTime:f}");
        await this.client!.EmitAsync("stints:join");
        await this.client!.EmitAsync("laps:join");
        await this.client!.EmitAsync("race_control:join");
    }

    private void Client_OnReconnectAttempt(object? sender, int e)
    {
        System.Diagnostics.Debug.Print($"###### Reconnect Attempt {DateTime.Now:f} #######");
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
