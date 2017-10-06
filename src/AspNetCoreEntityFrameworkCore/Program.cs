#define Expanded_Default // Expanded_Default, BuildWebHost_EnableEfConvention

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreEntityFrameworkCore.DataAccess;
using AspNetCoreEntityFrameworkCore.DataAccess.Model;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AspNetCoreEntityFrameworkCore
{
    public class Program
    {
#if Expanded_Default
        public static void Main(string[] args)
        {
            var webHost = new WebHostBuilder()
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
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                    logging.AddDebug();
                })
                .UseDefaultServiceProvider((context, options) =>
                {
                    options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
                })
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            InitializeDatabase(webHost.Services, webHost.Services.GetRequiredService<IHostingEnvironment>());

            webHost.Run();
        }
#endif

#if BuildWebHost_EnableEfConvention
        public static void Main(string[] args)
        {
            var webHost = BuildWebHost(args);

            InitializeDatabase(webHost.Services, webHost.Services.GetRequiredService<IHostingEnvironment>());

            webHost.Run();
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
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                    logging.AddDebug();
                })
                .UseDefaultServiceProvider((context, options) =>
                {
                    options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
                })
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();
#endif

        private static void InitializeDatabase(IServiceProvider serviceProvider, IHostingEnvironment env)
        {
            // QUESTE COSE NON SI FANNO IN PRODUZIONE!!!!
            if (env.IsDevelopment() == false)
            {
                return;
            }

            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var db = services.GetRequiredService<CommerceDbContext>();

                    UseEnsureCreated(db);
                    //UseMigration(db);
                }
                catch (Exception ex)
                {
                    var logger = services.GetService<ILogger<Program>>();
                    logger?.LogError(ex, "An error occurred while seeding the database.");
                }
            }
        }

        private static void UseEnsureCreated(CommerceDbContext db)
        {
            if (db.Database.EnsureCreated())
            {
                // add some data

                db.Products.Add(new Product
                {
                    Description = "il TV",
                    Price = 1234.56m
                });
                db.Products.Add(new Product
                {
                    Description = "Dash Button",
                    Price = 4.99m
                });

                db.SaveChanges();
            }
        }

        private static void UseMigration(CommerceDbContext db)
        {
            // Add-Migration InitialCreate

            db.Database.Migrate();

            // POSSO AGGIUNGERE I DATI COSI' OPPURE ATTRAVERSO UNA MIGRAZIONE

            var tv = db.Products.FirstOrDefault(x => x.Description == "il TV");
            if (tv == null)
            {
                db.Products.Add(new Product
                {
                    Description = "il TV",
                    Price = 1234.56m
                });
            }
            else
            {
                tv.Price = 1234.56m;
            }

            var dashButton = db.Products.FirstOrDefault(x => x.Description == "Dash Button");
            if (tv == null)
            {
                db.Products.Add(new Product
                {
                    Description = "Dash Button",
                    Price = 4.99m
                });
            }
            else
            {
                dashButton.Price = 4.99m;
            }

            db.SaveChanges();
        }
    }
}
