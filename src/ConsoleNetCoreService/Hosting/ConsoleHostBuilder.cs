using ConsoleNetCoreService.Hosting.Internal;

namespace ConsoleNetCoreService.Hosting
{
    public interface IConsoleHostBuilder
    {
        IConsoleHost Build();
    }

    public class ConsoleHostBuilder : IConsoleHostBuilder
    {
        public IConsoleHost Build()
        {
            var host = new ConsoleHost();
            host.Configure();
            return host;
        }
    }
}
