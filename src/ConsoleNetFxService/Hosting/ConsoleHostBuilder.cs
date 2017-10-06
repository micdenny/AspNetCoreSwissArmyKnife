using ConsoleNetFxService.Hosting.Internal;

namespace ConsoleNetFxService.Hosting
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
