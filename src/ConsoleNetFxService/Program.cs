using ConsoleNetFxService.Hosting;

namespace ConsoleNetFxService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildTopshelfHost(args).Run();
        }

        public static ITopshelfHost BuildTopshelfHost(string[] args) =>
            new TopshelfHostBuilder()
                .Build();
    }
}
