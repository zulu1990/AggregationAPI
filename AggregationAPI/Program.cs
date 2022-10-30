using AggregationAPI.Extensions;
using AggregationAPI.Models;
using AggretationApp.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace AggregationAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<DataContext>();
                    var config = services.GetRequiredService<IConfiguration>();
                    context.Database.Migrate();

                    if (!context.Records.Any())
                    {
                        var dataSource = new DataSource();
                        config.GetSection("DataSource").Bind(dataSource);
                        var data = DataSeeder.SeedData(dataSource).Result;

                        context.Records.AddRange(data);
                        context.SaveChanges();
                    }

                }
                catch (Exception e)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e, "An error occured during migration");
                    throw;
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
