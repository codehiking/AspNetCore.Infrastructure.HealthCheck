using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebApiExample
{
    public class Program
    {
        public static void Main (string[] args)
        {
            IWebHost webHost = new WebHostBuilder ()
                .UseKestrel ()
                .UseStartup<Startup> ()
                .UseContentRoot (Directory.GetCurrentDirectory ())
                .ConfigureAppConfiguration ((a, b)=> b
                    .AddJsonFile ("appsettings.json", false, true)
                    .AddJsonFile ($"appsettings.{a.HostingEnvironment.EnvironmentName}.json", true, true))
                .ConfigureLogging ((hostingContext, logging)=>
                {
                    logging.AddConfiguration (hostingContext.Configuration.GetSection ("Logging"));
                    logging.AddConsole ();
                    logging.AddDebug ();
                })
                .Build ();

            webHost.Run ();
        }
    }
}