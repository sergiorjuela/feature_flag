using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace app_config_web
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
                    webBuilder.ConfigureAppConfiguration(config =>
                    {
                        var settings = config.Build();

                        // obtenga la cadena de conexión
                        // del store de secretos
                        var connection = settings.GetConnectionString("AppConfig");

                        // use las opciones de App Configuration
                        config.AddAzureAppConfiguration(options =>
                            options.Connect(connection)
                                // use feature flags
                                .UseFeatureFlags());
                    }).UseStartup<Startup>());
    }
}
