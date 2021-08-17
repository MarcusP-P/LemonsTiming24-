using System.Globalization;

namespace LemonsTiming24.Server.Services.SocketIo;
public class EngineIoParser
{
    public EngineIoParser() : this("")
    {

    }

    public EngineIoParser(string inputString)
    {
        this.InputString = inputString;
    }

    public string InputString { get; init; }

    // A Socket.io http connection can have several http
    public EngineIoPacket[] ParseHttpResult()
    {
        var currentString = this.InputString;

        var result = new List<EngineIoPacket>();

        while (currentString.Length != 0)
        {
            // find the first :
            var seperatorPosition = currentString.IndexOf(':');

            // Anything before the first colon is the length
            var lengthString = currentString.Substring(0, seperatorPosition);
            var length = int.Parse(lengthString, NumberStyles.None, CultureInfo.InvariantCulture);

            // Trim the length from the start of the string
            currentString = currentString[(seperatorPosition + 1)..];

            // Get the data portion of the string
            var data = currentString.Substring(0, length);

            // Trim the current data from teh stat of our packet.
            currentString = currentString[length..];

            // Parse the data
            var resultData = CreateEngineIoPacket(data);

            result.Add(resultData);
        }

        return result.ToArray();
    }

    // WebSockets will send only one message per transaction
    public EngineIoPacket[] ParseWebSocketResult()
    {
        var result = new EngineIoPacket[]
        {
            CreateEngineIoPacket(this.InputString)
        };

        return result;
    }

    public static EngineIoPacket CreateEngineIoPacket(string data)
    {
        var messageTypeField = data.Substring(0, 1);
        var message = data[1..];

        var messageType = ConvertIntToFlag(messageTypeField);

        return new EngineIoPacket
        {
            Payload = message,
            Flags = messageType,
        };
    }

    public static EngineIoMessageType ConvertIntToFlag(string messageTypeField)
    {
        return messageTypeField switch
        {
            "0" => EngineIoMessageType.Open,
            "1" => EngineIoMessageType.Close,
            "2" => EngineIoMessageType.Ping,
            "3" => EngineIoMessageType.Pong,
            "4" => EngineIoMessageType.Message,
            "5" => EngineIoMessageType.Upgrade,
            "6" => EngineIoMessageType.NoOp,
            _ => throw new NotImplementedException(),
        };
    }
}
