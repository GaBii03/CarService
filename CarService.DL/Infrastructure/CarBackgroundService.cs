using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CarService.DL.Infrastructure
{
    internal class CarBackgroundService : BackgroundService
    {
        private readonly ILogger<CarBackgroundService> _logger;

        public CarBackgroundService(ILogger<CarBackgroundService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("CarBackgroundService is running. {Time}", DateTime.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
