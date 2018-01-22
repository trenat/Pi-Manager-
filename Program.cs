using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace PiManagment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //BuildWebHost(args).Run();

            var host = new WebHostBuilder()
            .UseKestrel()
            .UseUrls("http://*:8000")
            .UseIISIntegration()
            .ConfigureLogging(loggerFactory => loggerFactory
                .AddConsole()
                .AddDebug())
            .UseStartup<Startup>()
            .Build();

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://0.0.0.0:4999") // Take that, Docker port forwarding!!!
                .Build();
    }
}
