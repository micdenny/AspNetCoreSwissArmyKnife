using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace ConsoleNetCoreSerilog
{
    public class BloggingContext : DbContext
    {
        public BloggingContext(DbContextOptions<BloggingContext> options)
            : base(options)
        {
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
    }

    public interface IBloggingContextFactory
    {
        BloggingContext Create();
    }

    public class BloggingContextFactory : IBloggingContextFactory
    {
        private readonly ILoggerFactory _loggerFactory;

        public BloggingContextFactory(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public BloggingContext Create()
        {
            var builder = new DbContextOptionsBuilder<BloggingContext>()
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ConsoleNetCoreSerilog;Trusted_Connection=True;")
                .UseLoggerFactory(_loggerFactory);

            var options = builder.Options;

            return new BloggingContext(options);
        }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
        public int Rating { get; set; }
        public List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
