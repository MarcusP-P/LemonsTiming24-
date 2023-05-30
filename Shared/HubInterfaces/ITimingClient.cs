namespace LemonsTiming24.SharedCode.HubInterfaces;
public interface ITimingClient
{
    Task RecieveMessage(string message);
}
