
using System.Net.WebSockets;

namespace LemonsTiming24.Server.Infrastructure.WebSockets;
public class TimingConnection
{
    private readonly WebSocket connection;
    private readonly TaskCompletionSource<object?> connectionTcs;

    public Guid Id { get; }

    public TimingConnection(WebSocket connection, TaskCompletionSource<object?> connectionTcs)
    {
        this.connection = connection;
        this.connectionTcs = connectionTcs;

        this.Id = Guid.NewGuid();
    }

    public async Task CloseAsync()
    {
        if (this.connection.CloseStatus is not null && this.connection.State == WebSocketState.Open)
        {
            await this.connection.CloseAsync(WebSocketCloseStatus.NormalClosure, "Complete", new());
        }

        _ = this.connectionTcs.TrySetResult(null);

    }
}
