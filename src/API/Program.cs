using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Todo.Infrastructure.Persistence;

namespace CleanArchitecture.WebUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = BuildWebHost(args);

            // Find the service layer within our scope.
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var hostEnv = services.GetService<IWebHostEnvironment>();
                var config = services.GetService<IConfiguration>();

                if (hostEnv.IsDevelopment())
                {
                    try
                    {
                        // Get the instance of DBContext in our services layer
                        var context = services.GetRequiredService<ApplicationDbContext>();

                        //context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();

                        //if (context.Database.IsSqlServer())
                        //{
                        //    context.Database.Migrate();
                        //}

                        SeedDataGenerator.Initialize(services, config);
                    }
                    catch (Exception ex)
                    {
                        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                        logger.LogError(ex, "An error occurred while migrating or seeding the database.");

                        throw;
                    }
                }
            }

            await host.RunAsync();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSetting("detailedErrors", "true")
                .CaptureStartupErrors(true)
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddEventSourceLogger();
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                })
                .Build();
    }
}
