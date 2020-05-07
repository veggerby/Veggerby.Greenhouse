using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Veggerby.Greenhouse.Startup))]

namespace Veggerby.Greenhouse
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string connectionString = Environment.GetEnvironmentVariable("veggerbygreenhouse_demo_SQL");
            builder.Services.AddDbContext<GreenhouseContext>(options => options.UseSqlServer(connectionString, opt => opt.EnableRetryOnFailure()));

            /*builder.Services.AddOptions<MyOptions>()
                .Configure<IConfiguration>((settings, configuration) =>
                                           {
                                                configuration.GetSection("MyOptions").Bind(settings);
                                           });*/

            //builder.Services.AddLogging();
        }
    }
}