using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace ConsoleNetCoreDependencyInjection
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Application configuring...");

            var services = new ServiceCollection();

            services
                .AddLogging(x => x
                    .SetMinimumLevel(LogLevel.Trace)
                    .AddDebug()
                    .AddConsole());

            services
                .AddSingleton<IApplication, Application>()
                .AddSingleton<ISomeBusiness, SomeBusiness>()
                .AddSingleton<ISomeService, SomeService>();

            var serviceProvider = services.BuildServiceProvider();

            Console.WriteLine("Application configured!");

            Console.WriteLine();
            Console.WriteLine("Press enter to run the application...");
            Console.ReadLine();

            var app = serviceProvider.GetRequiredService<IApplication>();
            app.Run();
            
            Console.ReadLine();
        }
    }

    public interface IApplication
    {
        void Run();
    }

    public class Application : IApplication
    {
        private readonly ISomeBusiness _someBusiness;
        private readonly ILogger<Application> _logger;

        public Application(ISomeBusiness someBusiness, ILogger<Application> logger)
        {
            _someBusiness = someBusiness;
            _logger = logger;
        }

        public void Run()
        {
            _logger.LogInformation("Run()");
            _someBusiness.DoBusiness();
        }
    }

    public interface ISomeBusiness
    {
        void DoBusiness();
    }

    public class SomeBusiness : ISomeBusiness
    {
        private readonly ISomeService _someService;
        private readonly ILogger<SomeBusiness> _logger;

        public SomeBusiness(ISomeService someService, ILogger<SomeBusiness> logger)
        {
            _someService = someService;
            _logger = logger;
        }

        public void DoBusiness()
        {
            _logger.LogInformation("DoBusiness()");
            _someService.DoService();
        }
    }

    public interface ISomeService
    {
        void DoService();
    }

    public class SomeService : ISomeService
    {
        private readonly ILogger<SomeService> _logger;

        public SomeService(ILogger<SomeService> logger)
        {
            _logger = logger;
        }

        public void DoService()
        {
            _logger.LogInformation("DoService()");
        }
    }
}
