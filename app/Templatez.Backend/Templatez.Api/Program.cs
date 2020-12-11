using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.IO;

namespace Templatez.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                        .Enrich.FromLogContext()
                        .WriteTo.Console()
                        .CreateLogger();

                Log.Information($"Starting API on {DateTime.Now.ToString()}");

                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                //config.Sources.Clear();
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                      .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json",
                                     optional: true, reloadOnChange: true);
                config.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.Logs.json",
                        optional: true, reloadOnChange: true);

                config.AddEnvironmentVariables();

                if (args != null)
                    config.AddCommandLine(args);
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                    .UseSerilog((hostingContext, config) =>
                    {
                        config
                            .ReadFrom.Configuration(hostingContext.Configuration)
                            .Enrich.FromLogContext()
                            .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name)
                            .Enrich.WithProperty("Environment", hostingContext.HostingEnvironment);
                    })
                    .UseKestrel()
                    .ConfigureKestrel(options => { })
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    //.UseUrls("http://*:80")
                    .UseIISIntegration()
                    .CaptureStartupErrors(true)
                    .UseStartup<Startup>();
            });
    }
}
