using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FleetManagement.DAL;
using FleetManagement.DAL.DatabaseSeeding;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FleetManagement.API.Write
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((host, services) =>
                {
                    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    var isDevelopment = environment == Environments.Development;

                    if (isDevelopment)
                    {
                        var connectionString = host.Configuration.GetConnectionString("FleetManagementDatabase");

                        services.AddTransient<IDatabaseSeeder, DatabaseInitializer>();
                        services.AddDbContext<FleetManagementContext>(options =>
                        {
                            options.UseSqlServer(connectionString);
                        });

                        using var scope = services.BuildServiceProvider().CreateScope();
                        var initializer = scope.ServiceProvider.GetRequiredService<IDatabaseSeeder>();

                        initializer
                            .SeedDatabase()
                            .Wait();
                    }
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
