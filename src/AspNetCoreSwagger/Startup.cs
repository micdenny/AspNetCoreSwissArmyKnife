using AspNetCoreSwagger.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace AspNetCoreSwagger
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

            services.Configure<CartServiceOptions>(Configuration.GetSection("CartService"));
            services.Configure<ProductServiceOptions>(Configuration.GetSection("ProductService"));

            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IProductService, ProductService>();

            services.AddMvc();

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info
                {
                    Title = "ASP.NET Core Api",
                    Version = "v1"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            // espone lo swagger generato sulla rotta di default /swagger/v1/swagger.json
            app.UseSwagger();

            // espone la documentazione interattiva (UI) su rotta /swagger
            app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "ASP.NET Core Api v1"));
        }
    }
}
