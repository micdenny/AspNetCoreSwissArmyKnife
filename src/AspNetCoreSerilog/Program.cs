using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AspNetCoreSerilog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            new WebHostBuilder()
                .UseKestrel()
                .UseIISIntegration()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                    config.AddEnvironmentVariables();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    /* ASP.NET Core ships the following providers:
                     * 
                     * https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging?tabs=aspnetcore2x#built-in-logging-providers
                     * 
                     * Console
                     * Debug
                     * EventSource
                     * EventLog
                     * TraceSource
                     * Azure App Service
                     * 
                     */

                    var config = hostingContext.Configuration;

                    logging.AddConfiguration(config.GetSection("Logging"));
                    logging.AddConsole();
                    logging.AddDebug();

                    // log su file con serilog: solo 1 linea di codice per configurarlo!
                    logging.AddFile(config.GetSection("Logging"));
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
