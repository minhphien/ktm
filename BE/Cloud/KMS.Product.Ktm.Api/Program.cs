using KMS.Product.Ktm.Api.HostedService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KMS.Product.Ktm.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .ConfigureAppConfiguration((context, appConfig) => 
                appConfig
                .AddEnvironmentVariables()
                .AddAzureAppConfiguration(appConfig.Build()["ConnectionStrings:AppConfig"], true)
            )
            .ConfigureServices(services =>
            {
                //services.AddHostedService<EmailHostedService>();
            });
    }
}
