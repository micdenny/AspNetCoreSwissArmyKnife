using System;
using System.IO;

namespace ConsoleNetFxService.Hosting.Internal
{
    public class HostingEnvironment : IHostingEnvironment
    {
        public string EnvironmentName { get; set; } = Hosting.EnvironmentName.Production;

        public string ContentRootPath { get; set; } = Directory.GetCurrentDirectory();

        public HostingEnvironment()
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (!string.IsNullOrWhiteSpace(envName))
            {
                this.EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            }
        }
    }
}
