using BugBountyRadar.Api.Application.Import;

namespace BugBountyRadar.Api.Background;

public class HackerOneSyncJob(IServiceProvider sp, ILogger<HackerOneSyncJob> log)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            using var scope = sp.CreateScope();
            await scope.ServiceProvider.GetRequiredService<HackerOneImporter>()
                .ImportAsync();

            await Task.Delay(TimeSpan.FromHours(24), ct);
        }
    }
}