
namespace LemonsTiming24.Server.Services.BackgroundProcessing;
public interface ITimingDataFetcher
{
    Task DoWork(CancellationToken cancellationToken);
}
