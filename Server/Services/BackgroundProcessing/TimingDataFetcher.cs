using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using File = System.IO.File;
using Microsoft.Extensions.Options;
using LemonsTiming24.Server.Services.SocketIo;
using LemonsTiming24.Server.Infrastructure;

namespace LemonsTiming24.Server.Services.BackgroundProcessing;
public class TimingDataFetcher : ITimingDataFetcher, IDisposable
{
    private readonly IOptions<TimingConfiguration> timingConfiguration;
    private string sid = "";
    private bool useWebSocket;
    private bool connectionUpgraded;

    private ClientWebSocket webSocket = null!;
    private HttpClient http = null!;

    public TimingDataFetcher(IOptions<TimingConfiguration> timingConfiguration)
    {
        this.timingConfiguration = timingConfiguration;
    }

    public async Task DoWork(CancellationToken cancellationToken)
    {
        this.useWebSocket = false;
        this.webSocket = new ClientWebSocket();
        this.http = new HttpClient { BaseAddress = new Uri($"{this.timingConfiguration.Value.BaseUrl}/{this.timingConfiguration.Value.SocketPath}/") };

        // The timing server uses Engine.Io and Socket.Io to exchange data.
        // The lower level Engine.Io protocol is described at https://github.com/socketio/engine.io-protocol/tree/v3
        // The higher level Socket.Io protocol is described at https://github.com/socketio/socket.io-protocol/tree/v4
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                var randomString = GetRandomString(7);

                var configurationResponse = await this.http.GetStringAsync($"?EIO=3&transport=polling&t={randomString}", cancellationToken);

                var rawParser = new EngineIoParser(configurationResponse);
                var parsedResult = rawParser.ParseHttpResult();

                foreach (var packet in parsedResult)
                {
                    await this.HandleEngineIoPacket(packet, cancellationToken);
                }

                randomString = GetRandomString(7);
                var raceResponse = await this.http.GetStringAsync($"?EIO=3&transport=polling&t={randomString}&sid={this.sid}", cancellationToken);

                rawParser = new EngineIoParser(raceResponse);
                parsedResult = rawParser.ParseHttpResult();

                foreach (var packet in parsedResult)
                {
                    await this.HandleEngineIoPacket(packet, cancellationToken);
                }
                /*
                            randomString = GetRandomString(7);
                            await this.webSocket.ConnectAsync(new Uri($"wss://data.fiawec.6tm.eu/socket.io/?EIO=3&transport=websocket&t={randomString}&sid={this.sid}"), cancellationToken);
                            this.useWebSocket = true;
                            this.connectionUpgraded = false;

                            await this.SendMessage(new EngineIoPacket
                            {
                                Flags = EngineIoMessageType.Ping,
                                Payload = "probe",
                            }, cancellationToken);

                            var counter = 0;

                            var buffer = new ArraySegment<byte>(new byte[65536]);
                            var result = await this.webSocket.ReceiveAsync(buffer, cancellationToken);

                            while (!cancellationToken.IsCancellationRequested && !result.CloseStatus.HasValue)
                            {
                                // Note that the received block might only be part of a larger message. If this applies in your scenario,
                                // check the received.EndOfMessage and consider buffering the blocks until that property is true.
                                var receivedAsText = Encoding.UTF8.GetString(buffer.Array!, 0, result.Count);

                                rawParser = new EngineIoParser(receivedAsText);
                                parsedResult = rawParser.ParseWebSocketResult();

                                foreach (var packet in parsedResult)
                                {
                                    await this.HandleEngineIoPacket(packet, cancellationToken);
                                }

                                counter++;

                                if (counter == 10)
                                {
                                    await this.SendMessage(new EngineIoPacket
                                    {
                                        Flags = EngineIoMessageType.Ping,
                                        Payload = "probe",
                                    }, cancellationToken);

                                    counter = 0;
                                }

                                result = await this.webSocket.ReceiveAsync(buffer, cancellationToken);
                            }
                */
                await Task.Delay(1000, cancellationToken);

            }
            // If we get an exception, we jsut want to go through again.
            catch
            {

            }
        }

        static string GetRandomString(int length)
        {
            var stringBuilder = new StringBuilder();

            while (stringBuilder.Length < length)
            {
                _ = stringBuilder.Append(Path.GetRandomFileName().Replace(".", ""));
            }

            var result = stringBuilder.ToString(0, length);

            return result;
        }
    }

    private async Task HandleEngineIoPacket(EngineIoPacket packet, CancellationToken cancellationToken)
    {
        switch (packet.Flags)
        {
            case EngineIoMessageType.Open:
                System.Diagnostics.Debug.Print("###### Open");
                var configurationJson = JsonDocument.Parse(packet.Payload);

                //await WriteJsonDocument(timeStamp, "configuration", configurationJson);

                this.sid = configurationJson.RootElement.GetProperty("sid").GetString() ?? "";
                this.connectionUpgraded = this.useWebSocket == false;

                break;
            case EngineIoMessageType.Close:
                System.Diagnostics.Debug.Print("###### Close");
                break;
            case EngineIoMessageType.Ping:
                System.Diagnostics.Debug.Print("###### Ping");
                await this.SendMessage(new EngineIoPacket
                {
                    Flags = EngineIoMessageType.Pong,
                    Payload = packet.Payload,
                }, cancellationToken);

                break;
            case EngineIoMessageType.Pong:
                System.Diagnostics.Debug.Print("###### Pong");
                if (this.useWebSocket && !this.connectionUpgraded)
                {
                    await this.SendMessage(new EngineIoPacket
                    {
                        Flags = EngineIoMessageType.Upgrade,
                        Payload = "",
                    }, cancellationToken);
                    this.connectionUpgraded = true;
                }

                break;
            case EngineIoMessageType.Message:
                System.Diagnostics.Debug.Print("###### Message");
                var data = packet.Payload;
                if (this.connectionUpgraded)
                {
                    // Just assume the socket.io packets are all Event with no ACK id
                    // TODO: Handle Socket.Io packets.
                    data = data[1..];

                    // TODO: Handle message
                    if (data.Length > 0)
                    {
                        await this.WriteJsonDocument(data);
                    }
                }

                break;
            case EngineIoMessageType.Upgrade:
                System.Diagnostics.Debug.Print("###### Upgrade");
                if (this.useWebSocket)
                {
                    this.connectionUpgraded = true;
                }

                break;
            case EngineIoMessageType.NoOp:
                System.Diagnostics.Debug.Print("###### NoOp");
                break;
            default:
                throw new NotImplementedException();
        }
    }
    private async Task SendMessage(EngineIoPacket packet, CancellationToken cancellationToken)
    {
        var data = string.Concat(packet.Flags switch
        {
            EngineIoMessageType.Open => "0",
            EngineIoMessageType.Close => "1",
            EngineIoMessageType.Ping => "2",
            EngineIoMessageType.Pong => "3",
            EngineIoMessageType.Message => "4",
            EngineIoMessageType.Upgrade => "5",
            EngineIoMessageType.NoOp => "6",
            _ => throw new NotImplementedException(),
        }, packet.Payload);
        if (!this.useWebSocket)
        {
            throw new NotImplementedException();
        }
        else
        {
            var dataToSend = new ArraySegment<byte>(Encoding.UTF8.GetBytes(data));
            await this.webSocket.SendAsync(dataToSend, WebSocketMessageType.Text, true, cancellationToken);
        }
    }

    private async Task WriteJsonDocument(string document)
    {
        var name = "unknown";
        var extension = "txt";
        try
        {
            var jsonDocument = JsonDocument.Parse(document);

            try
            {
                extension = "json";
                var root = jsonDocument.RootElement;

                if (root.ValueKind == JsonValueKind.Array)
                {
                    foreach (var currentNode in root.EnumerateArray())
                    {
                        if (currentNode.ValueKind == JsonValueKind.String)
                        {
                            name = currentNode.GetString();
                            if (name is not null)
                            {
                                name = string.Join("", name.Split(Path.GetInvalidFileNameChars()));

                                break;
                            }
                        }
                    }
                }
            }
            catch
            {
            }

            document = JsonSerializer.Serialize(jsonDocument, new JsonSerializerOptions { WriteIndented = true });

        }
        catch
        {
        }

        var fileName = $"{name}-{DateTime.UtcNow:o}.{extension}".Replace(":", "-");

        var path = Path.Join(this.timingConfiguration.Value.SavedMessagesPath, fileName);

        await File.WriteAllTextAsync(path, document);
    }

    public void Dispose()
    {
        this.http?.Dispose();
        this.webSocket?.Dispose();

        GC.SuppressFinalize(this);
    }
}
