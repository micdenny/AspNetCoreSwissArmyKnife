using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleNetCoreService.Hosting
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder Run(this IApplicationBuilder app, Action runDelegate)
        {
            app.Use(next =>
            {
                return () =>
                {
                    runDelegate();
                    return Task.CompletedTask;
                };
            });
            return app;
        }

        public static IApplicationBuilder UseServiceApplication(this IApplicationBuilder app)
        {
            app.Use(next =>
            {
                return () =>
                {
                    var service = app.ApplicationServices.GetRequiredService<IApplicationService>();
                    service.Start();

                    var lifetime = app.ApplicationServices.GetRequiredService<IApplicationLifetime>();
                    lifetime.ApplicationStopping.Register(() =>
                    {
                        service.Stop();
                    });

                    return next();
                };
            });
            return app;
        }
    }
}
