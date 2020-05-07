using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Veggerby.Greenhouse.Core;

namespace Veggerby.Greenhouse.Migrations
{
    class Program
    {

        static void Main(string[] args)
        {
            var context = new GreenhouseContextFactory().CreateDbContext(args);
            context.Database.Migrate();
        }
    }

    public class GreenhouseContextFactory : IDesignTimeDbContextFactory<GreenhouseContext>
    {
        private static IConfiguration GetConfiguration()
        {
            var path = Directory.GetCurrentDirectory();

            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            return config;
        }

        public GreenhouseContext CreateDbContext(string[] args)
        {
            var connectionString = GetConfiguration()["sql:connection"];
            var optionsBuilder = new DbContextOptionsBuilder<GreenhouseContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new GreenhouseContext(optionsBuilder.Options);
        }
    }
}
