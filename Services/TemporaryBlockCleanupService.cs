using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

public class TemporaryBlockCleanupService : BackgroundService
{
    private readonly BlockingService _service;
    public TemporaryBlockCleanupService(BlockingService service) => _service = service;
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _service.CleanupExpiredBlocks();
            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
}