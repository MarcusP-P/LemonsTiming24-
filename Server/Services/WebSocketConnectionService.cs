using LemonsTiming24.Server.Infrastructure.WebSockets;
using System.Collections.Concurrent;

namespace LemonsTiming24.Server.Services;
public class WebSocketConnectionService : IWebSocketConnectionService
{
    private readonly ConcurrentDictionary<Guid, TimingConnection> connections;

    public WebSocketConnectionService()
    {
        this.connections = new();
    }

    public void AddConnection(TimingConnection connection)
    {
        _ = this.connections.TryAdd(connection.Id, connection);
    }

    public async Task CloseConnectionAsync(Guid Id)
    {
        if (this.connections.TryRemove(Id, out var connection))
        {
            await connection.CloseAsync();
        }

    }
}
