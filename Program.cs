using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book_store_v1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Log.Information("This is start");
                var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();


                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(config)
                    .CreateLogger();

                //Log.Logger = new LoggerConfiguration()
                // .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
                // .CreateLogger();

                CreateHostBuilder(args).Build().Run();

            }
            catch(Exception ex)
            {
                Log.Fatal(ex.Message, "This is faild log");
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
