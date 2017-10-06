using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Collections;
using System.Runtime.CompilerServices;

namespace ConsoleNetCoreSerilog
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();

            services.AddLogging(builder => builder
                .SetMinimumLevel(LogLevel.Trace)
                .AddFilter("System", LogLevel.Information)
                .AddFilter("Microsoft", LogLevel.Information)
                .AddConsole()
                .AddFile("Logs/{Date}.log"));

            services.AddSingleton<IBloggingContextFactory, BloggingContextFactory>();
            services.AddSingleton<Application>();

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<Application>().Run();

            (serviceProvider as IDisposable)?.Dispose();
        }

        public class Application
        {
            private readonly ILogger<Application> _logger;
            private readonly IBloggingContextFactory _bloggingContextFactory;

            public Application(ILogger<Application> logger, IBloggingContextFactory bloggingContextFactory)
            {
                _logger = logger;
                _bloggingContextFactory = bloggingContextFactory;
            }

            public void Run()
            {
                CreateNewBlogAndReturnAllBlogs();
            }

            public void CreateNewBlogAndReturnAllBlogs()
            {
                using (_logger.BeginScope(new RequestLogScope()))
                {
                    InitDatabase();

                    CreateBlog();

                    ReadAllBlogs();
                }
            }

            public void InitDatabase()
            {
                using (var db = _bloggingContextFactory.Create())
                {
                    db.Database.EnsureCreated();
                }
            }

            public void CreateBlog()
            {
                using (var db = _bloggingContextFactory.Create())
                {
                    _logger.LogInformation("Add new blog");

                    var blog = new Blog()
                    {
                        Url = "http://dennymichael.net",
                        Rating = 100
                    };
                    db.Blogs.Add(blog);
                    db.SaveChanges();
                }
            }

            public void ReadAllBlogs()
            {
                using (var db = _bloggingContextFactory.Create())
                {
                    _logger.LogInformation("Reading all the blogs...");
                    foreach (var b in db.Blogs)
                    {
                        Console.WriteLine($"{b.BlogId} - Url: {b.Url} Rating: {b.Rating}");
                    }
                }
            }
        }

        // La proprietà RequestId è quella già inclusa di default nel template dell'estensione "Serilog.Extensions.Logging.File"
        // è il nome che viene utilizzato anche da asp.net core per aggiungere l'id di richiesta
        public class RequestLogScope : IReadOnlyList<KeyValuePair<string, object>>
        {
            private string _cachedToString;

            private readonly string _requestId;

            public int Count
            {
                get
                {
                    return 1;
                }
            }

            public KeyValuePair<string, object> this[int index]
            {
                get
                {
                    if (index == 0)
                    {
                        return new KeyValuePair<string, object>("RequestId", _requestId);
                    }

                    throw new ArgumentOutOfRangeException(nameof(index));
                }
            }

            public RequestLogScope([CallerMemberName] string correlationId = null)
            {
                _requestId = $"{correlationId}-{Guid.NewGuid().ToString()}";
            }

            public override string ToString()
            {
                if (_cachedToString == null)
                {
                    _cachedToString = string.Format(
                        CultureInfo.InvariantCulture,
                        "RequestId:{0}",
                        _requestId);
                }

                return _cachedToString;
            }

            public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
            {
                for (int i = 0; i < Count; ++i)
                {
                    yield return this[i];
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}
