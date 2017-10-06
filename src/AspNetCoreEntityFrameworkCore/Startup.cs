using AspNetCoreEntityFrameworkCore.DataAccess;
using AspNetCoreEntityFrameworkCore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace AspNetCoreEntityFrameworkCore
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
            services.AddOptions();

            services.AddDbContext<CommerceDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CommerceConnection")));

            // da utilizzare per migliorare le prestazioni, ma ha delle limitazioni, vedi: https://docs.microsoft.com/en-us/ef/core/what-is-new/#high-performance
            //services.AddDbContextPool<CommerceDbContext>();

            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IProductService, ProductService>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Title = "ASP.NET Core Entity Framework Api",
                    Version = "v1"
                });
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "ASP.NET Core Entity Framework Api"));

            // NON METTERE PIU' IL CODICE DI INIZIALIZZAZIONE DATABASE QUI, MA IN PROGRAM MAIN!!
        }
    }
}
