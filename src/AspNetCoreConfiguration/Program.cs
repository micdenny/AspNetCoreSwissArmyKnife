using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AspNetCoreConfiguration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            new WebHostBuilder()
                .UseConfiguration(new ConfigurationBuilder().AddCommandLine(args).Build()) // <- questo abilita la possibilità di cambiare le configurazioni dell'host da command line
                .UseKestrel()
                .UseIISIntegration()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    /* ASP.NET Core ships the following configuration providers:
                     * 
                     * https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration?tabs=basicconfiguration
                     * 
                     * File formats (INI, JSON, and XML)
                     * Command-line arguments
                     * Environment variables
                     * In-memory .NET objects
                     * An encrypted user store
                     * Azure Key Vault
                     * Custom providers, which you install or create
                     * 
                     */

                    var env = hostingContext.HostingEnvironment;

                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

                    config.AddEnvironmentVariables();

                    if (args != null)
                    {
                        // dotnet AspNetCoreConfiguration.dll Server.Urls=http://*:34685 CartService:BuyTimeout=00:05:00
                        //config.AddCommandLine(args);

                        // dotnet AspNetCoreConfiguration.dll --server.urls http://*:34685 --buy-timeout 00:05:00
                        config.AddCommandLine(args, new Dictionary<string, string> { { "--buy-timeout", "CartService:BuyTimeout" } });
                    }
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                    logging.AddDebug();
                    logging.AddFile(hostingContext.Configuration.GetSection("Logging"));
                })
                .UseDefaultServiceProvider((context, options) =>
                {
                    options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
                })
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();
    }
}
