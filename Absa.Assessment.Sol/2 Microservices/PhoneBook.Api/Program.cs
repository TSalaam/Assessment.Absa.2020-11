using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using NLog.Web;

using PhoneBook.Api.Repositories;
using PhoneBook.Api.Repositories.Helpers;

namespace PhoneBook.Api {

    /// <summary>
    /// 
    /// </summary>
    public class Program {

        /// <summary>
        /// 
        /// </summary>
        public static void Main(string[] args) {

            var host = CreateHostBuilder(args).Build();

            //using (var scope = host.Services.CreateScope()) {

            //    var services = scope.ServiceProvider;

            //    try {
            //        var context = services.GetRequiredService<PhoneBookContext>();

            //        context.Database.Migrate();
            //        context.Database.EnsureCreatedAsync();

            //        SeedData.Initialize(services);
            //    }
            //    catch (Exception ex) {

            //        var logger = services.GetRequiredService<ILogger<Program>>();

            //        logger.LogError(ex, "An error occurred seeding the database");
            //    }
            //}

            host.Run();
        }

        /// <summary>
        /// 
        /// </summary>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(logging => {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                })
                .UseNLog();
    }
}
