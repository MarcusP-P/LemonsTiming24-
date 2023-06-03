namespace LemonsTiming24.Server.Services.BackgroundProcessing;
public class TimingDataFetcherHostedService : BackgroundService
{
    private readonly IServiceProvider serviceProvider;

    public TimingDataFetcherHostedService(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await this.DoWork(stoppingToken).ConfigureAwait(false);
    }

    private async Task DoWork(CancellationToken cancellationToken)
    {
        using var scope = this.serviceProvider.CreateScope();
        var scopedProcessingService = scope.ServiceProvider.GetRequiredService<ITimingDataFetcher>();

        await scopedProcessingService.DoWork(cancellationToken).ConfigureAwait(false);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await base.StopAsync(cancellationToken).ConfigureAwait(false);
    }
}
