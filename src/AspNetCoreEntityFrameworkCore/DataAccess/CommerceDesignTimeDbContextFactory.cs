using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AspNetCoreEntityFrameworkCore.DataAccess
{
    public class CommerceDesignTimeDbContextFactory //: IDesignTimeDbContextFactory<CommerceDbContext>
    {
        public CommerceDbContext CreateDbContext(string[] args)
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddEnvironmentVariables();

            var configuration = configBuilder.Build();

            var connString = configuration.GetConnectionString("CommerceConnection");

            var dbOptions = new DbContextOptionsBuilder<CommerceDbContext>()
                .UseSqlServer(connString)
                .Options;

            var dbContext = new CommerceDbContext(dbOptions);

            return dbContext;
        }
    }
}
