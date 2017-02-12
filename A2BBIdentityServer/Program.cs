using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using A2BBCommon;

namespace A2BBIdentityServer
{
    /// <summary>
    /// Application main entry class.
    /// </summary>
    public class Program
    {
        #region Public static methods
        /// <summary>
        /// Application main entry point.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls(Constants.IDENTITY_SERVER_ENDPOINT)
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
