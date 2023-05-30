namespace LemonsTiming24.SharedCode.HubInterfaces;
public interface ITimingHub
{
    Task SendMessage(string message);
}
