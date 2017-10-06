using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Topshelf;

namespace ConsoleNetFxService.Hosting.Internal
{
    public class TopshelfHost : ITopshelfHost
    {
        private TopshelfApplication _topshelfApplication;

        public void Configure(TopshelfApplication topshelfApplication)
        {
            _topshelfApplication = topshelfApplication;
        }

        public void Run()
        {
            HostFactory.Run(hostConfig =>
            {
                hostConfig.Service<TopshelfApplication>(serviceConfig =>
                {
                    serviceConfig.ConstructUsing(host => _topshelfApplication);
                    serviceConfig.WhenStarted(app => app.Start());
                    serviceConfig.WhenStopped(app => app.Stop());
                });

                hostConfig.RunAsLocalSystem();

                hostConfig.SetServiceName("ConsoleNetFxService");
                hostConfig.SetDisplayName("Console Net Fx Service");
                hostConfig.SetDescription("Console Net Fx Service.");

                hostConfig.StartManually();
            });
        }
    }

    public class TopshelfApplication
    {
        private IServiceProvider _serviceProvider;
        private ApplicationLifetime _applicationLifetime;
        private IApplicationBuilder _applicationBuilder;
        private ILogger<TopshelfHost> _logger;

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

            _logger = _serviceProvider.GetRequiredService<ILogger<TopshelfHost>>();

            _applicationBuilder = _serviceProvider.GetRequiredService<IApplicationBuilder>();
            _applicationLifetime = _serviceProvider.GetRequiredService<IApplicationLifetime>() as ApplicationLifetime;
            var loggerFactory = _serviceProvider.GetRequiredService<ILoggerFactory>();

            startup.Configure(_applicationBuilder, env, loggerFactory, _applicationLifetime);
        }

        public void Start()
        {
            if (_serviceProvider == null)
                throw new Exception("You must run ConsoleHost.Configure() before calling ConsoleHost.Run()");

            _logger.LogInformation("Host starting...");

            var app = _applicationBuilder.Build();
            app();

            _logger.LogInformation("Host started!");

            _applicationLifetime.NotifyStarted();
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
