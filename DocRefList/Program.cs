using System;
using System.IO;
using System.Threading.Tasks;
using DocRefList.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace DocRefList
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();


        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.RollingFile(
                    "DocRefList.LOG\\DocRefList[{Date}].log",
                    outputTemplate: "{Timestamp:HH:mm:ss}|{Level:u4}|{SourceContext}|{Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            ILogger logger = Log.ForContext<Program>();

            try
            {
                logger.Information("DocRefList Web starting up");
                await CreateHostBuilder(args).Build().RunWithTasksAsync();
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "DocRefList Web start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
