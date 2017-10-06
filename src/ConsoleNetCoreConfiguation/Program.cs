using ConsoleNetCoreConfiguation.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleNetCoreConfiguation
{
    class Program
    {
        static void Main(string[] args)
        {
            // --- BUILD CONFIGURATION
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            if (args != null)
            {
                // dotnet .\bin\Debug\netcoreapp2.0\AspNetCoreConfiguration.dll Server.Urls=http://0.0.0.0:34685 CartService:BuyTimeout=00:05:00
                //configBuilder.AddCommandLine(args);

                // dotnet .\bin\Debug\netcoreapp2.0\AspNetCoreConfiguration.dll --server.urls http://0.0.0.0:34685 --buy-timeout 00:05:00
                configBuilder.AddCommandLine(args, new Dictionary<string, string> { { "--buy-timeout", "CartService:BuyTimeout" } });
            }

            var configuration = configBuilder.Build();
            // ---

            var services = new ServiceCollection();

            services.AddLogging(builder => BuildLogging(builder));

            // --- ADD OPTIONS
            services.AddOptions();

            services.Configure<CartServiceOptions>(configuration.GetSection("CartService"));
            // ---

            services.AddSingleton<ICartService, CartService>();
            services.AddSingleton<Application>();

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<Application>().Run().Wait();

            (serviceProvider as IDisposable)?.Dispose();
        }

        private static ILoggingBuilder BuildLogging(ILoggingBuilder builder) =>
            builder
                .SetMinimumLevel(LogLevel.Trace)
                .AddFilter("System", LogLevel.Information)
                .AddFilter("Microsoft", LogLevel.Information)
                .AddConsole();
    }

    public class Application
    {
        private readonly ICartService _cartService;

        public Application(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task Run()
        {
            await _cartService.BuyCart(200);

            await _cartService.RemoveItem(200, 11);
        }
    }
}
