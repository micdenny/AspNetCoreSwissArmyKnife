using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AspNetCoreRequestLogger
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "ASP.NET Core Request Logger",
                    Version = "v1"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // lo installo qui invece che nel Program.Main perchè devo ancora aggiornare l'estensione al netcore v2.0.0
            //loggerFactory.AddApplicationInsights(Configuration.GetSection("Logging"), Configuration.GetSection("ApplicationInsights"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // l'ordine è importante, deve essere messo prima di UseMvc() altrimenti MVC non inoltra la chiamata al logger.
            //app.UseRequestLogger();

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(setup => setup.SwaggerEndpoint("/swagger/v1/swagger.json", "ASP.NET Core Request Logger"));
        }
    }
}
