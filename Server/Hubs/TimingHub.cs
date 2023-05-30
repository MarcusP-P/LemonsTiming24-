using LemonsTiming24.SharedCode.HubInterfaces;
using Microsoft.AspNetCore.SignalR;

namespace LemonsTiming24.Server.Hubs;

public class TimingHub : Hub<ITimingClient>, ITimingHub
{
    public async Task SendMessage(string message)
    {
        await this.Clients.All.RecieveMessage(message);
    }
}
