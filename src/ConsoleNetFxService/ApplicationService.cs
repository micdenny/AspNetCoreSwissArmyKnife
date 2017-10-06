using System.Threading;
using ConsoleNetFxService.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConsoleNetFxService
{
    public class ApplicationService : IApplicationService
    {
        private Thread _thread;
        private CancellationTokenSource _cts = new CancellationTokenSource();

        private readonly ILogger<ApplicationService> _logger;

        public ApplicationService(ILogger<ApplicationService> logger)
        {
            _logger = logger;
        }

        public void Start()
        {
            _logger.LogInformation("Application service starting...");
            var cancellationToken = _cts.Token;
            _thread = new Thread(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    _logger.LogInformation("ping pong...");
                    if (!cancellationToken.IsCancellationRequested) Thread.Sleep(1000);
                }
            });
            _thread.Start();
            _logger.LogInformation("Application service started!");
        }

        public void Stop()
        {
            _logger.LogInformation("Application service stopping...");
            _cts.Cancel();
            _thread.Join();
            _logger.LogInformation("Application service stopped!");
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddSingleton<IApplicationService, ApplicationService>();
            return services;
        }
    }
}
