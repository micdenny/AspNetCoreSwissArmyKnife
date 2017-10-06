using ConsoleNetFxService.Hosting.Internal;

namespace ConsoleNetFxService.Hosting
{
    public interface ITopshelfHostBuilder
    {
        ITopshelfHost Build();
    }

    public class TopshelfHostBuilder : ITopshelfHostBuilder
    {
        public ITopshelfHost Build()
        {
            var host = new TopshelfHost();
            var topshelfApp = new TopshelfApplication();
            topshelfApp.Configure();
            host.Configure(topshelfApp);
            return host;
        }
    }
}
