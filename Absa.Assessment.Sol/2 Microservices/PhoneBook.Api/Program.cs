using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using NLog.Web;

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
