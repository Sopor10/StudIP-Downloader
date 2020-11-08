using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StudIPDownloader.WebApi.Controllers;

namespace StudIPDownloader.WebApi.HostedServices
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<TimedHostedService> _logger;
        private Timer _timer;
        private int executionCount;

        public TimedHostedService(ILogger<TimedHostedService> logger, StudipDownloadController studipDownloadController, InputContract input)
        {
            StudipDownloadController = studipDownloadController;
            Input = input;
            _logger = logger;
        }

        public StudipDownloadController StudipDownloadController { get; }
        public InputContract Input { get; }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,Input.TimeSpan);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var count = Interlocked.Increment(ref executionCount);

            _logger.LogInformation(
                $"Timed Hosted Service is working. Count: {count}");


            StudipDownloadController.Get(Input);
        }
    }
}