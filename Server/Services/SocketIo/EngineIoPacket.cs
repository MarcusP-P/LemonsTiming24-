
namespace LemonsTiming24.Server.Services.SocketIo;
public class EngineIoPacket
{
    public EngineIoMessageType Flags { get; set; }
    public string Payload { get; set; } = "";
}
