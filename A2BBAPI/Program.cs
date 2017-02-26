using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using A2BBCommon;

namespace A2BBAPI
{
    /// <summary>
    /// Main entry class of the application.
    /// </summary>
    public class Program
    {
        #region Public static methods
        /// <summary>
        /// Main entry point of the application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls(Constants.API_ENDPOINT)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }
        #endregion
    }
}
