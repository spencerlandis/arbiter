using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Threading.Tasks;

namespace Arbiter
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            using var app = CreateHostBuilder(args).Build();
            var logger = app.Services.GetService<ILogger<Program>>();
            logger.LogInformation("Starting Arbiter");
            await app.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                    builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables("ARBITER_")
                )
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseSerilog((context, configure) =>
                    {
                        configure.ReadFrom.Configuration(context.Configuration)
                            .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName);
                    });

                    webBuilder.UseStartup<Startup>();
                });
    }
}
