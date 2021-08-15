
using LemonsTiming24.Server.Infrastructure.WebSockets;

namespace LemonsTiming24.Server.Services;
public interface IWebSocketConnectionService
{
    void AddConnection(TimingConnection connection);

    Task CloseConnectionAsync(Guid Id);

}
