using System;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConsoleNetFxService.Hosting.Internal
{
    public class ConsoleHost : IConsoleHost
    {
        private IServiceProvider _serviceProvider;
        private ApplicationLifetime _applicationLifetime;
        private IApplicationBuilder _applicationBuilder;
        private ILogger<ConsoleHost> _logger;

        public void Configure()
        {
            var env = new HostingEnvironment();

            var services = new ServiceCollection();
            services.AddSingleton<IHostingEnvironment, HostingEnvironment>();
            services.AddSingleton<IApplicationLifetime, ApplicationLifetime>();
            services.AddSingleton<IApplicationBuilder, ApplicationBuilder>();
            services.AddLogging();

            var startup = new Startup(env);

            startup.ConfigureServices(services);

            _serviceProvider = services.BuildServiceProvider();

            _logger = _serviceProvider.GetRequiredService<ILogger<ConsoleHost>>();

            _applicationBuilder = _serviceProvider.GetRequiredService<IApplicationBuilder>();
            _applicationLifetime = _serviceProvider.GetRequiredService<IApplicationLifetime>() as ApplicationLifetime;
            var loggerFactory = _serviceProvider.GetRequiredService<ILoggerFactory>();

            startup.Configure(_applicationBuilder, env, loggerFactory, _applicationLifetime);
        }

        public void Run()
        {
            if (_serviceProvider == null)
                throw new Exception("You must run ConsoleHost.Configure() before calling ConsoleHost.Run()");

            _logger.LogInformation("Host starting...");

            var appExited = new ManualResetEvent(false);

            AppDomain.CurrentDomain.ProcessExit += (sender, e) =>
            {
                // graceful shutdown
                Stop();
                _logger.LogInformation("Bye bye!");
                appExited.Set();
            };
            Console.CancelKeyPress += (sender, e) =>
            {
                // graceful shutdown
                Stop();
                _logger.LogInformation("Bye bye!");
                appExited.Set();
            };

            var app = _applicationBuilder.Build();
            app();

            _logger.LogInformation("Host started!");

            _applicationLifetime.NotifyStarted();

            appExited.WaitOne();
        }

        public void Stop()
        {
            _logger.LogInformation("Stopping host...");
            _applicationLifetime?.NotifyStopping();
            (_serviceProvider as IDisposable)?.Dispose();
            _applicationLifetime?.NotifyStopped();
            _logger.LogInformation("Host stopped!");
        }
    }
}
