using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CarService.DL.Infrastructure
{
    internal class CarHostedService : IHostedService
    {
        private readonly ILogger<CarHostedService> _logger;

        public CarHostedService(ILogger<CarHostedService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    _logger.LogInformation("CarHostedService is running. {Time}", DateTime.Now);
                    await Task.Delay(1000, cancellationToken);
                }
            }, cancellationToken);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
