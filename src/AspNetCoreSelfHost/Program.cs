using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AspNetCoreSelfHost
{
    // tutte le application asp.net core sono già self-hosted by-design, cambia solo il tipo di progetto per abilitare funzionalità legate strettamente al web (bower, bundling and minification, ...)
    class Program
    {
        static void Main(string[] args)
        {
            var webHost = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                    logging.AddDebug();
                })
                .UseStartup<Startup>()
                .Build();

            webHost.Run();
        }
    }

    public class Startup
    {
        public Startup()
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }

    [Route("api/[controller]")]
    public class CartController : Controller
    {
        [HttpGet]
        public string Get()
        {
            // return the cart with all its items
            return "This is the cart with all its items, don't you see it?!";
        }

        [HttpPut("{itemId}")]
        public void Put(int itemId)
        {
            // add an item to the cart
        }

        [HttpPost]
        public void Post()
        {
            // buy all the items in the cart
        }

        [HttpDelete("{itemId}")]
        public void Delete(int itemId)
        {
            // remove an item from the cart
        }
    }
}
