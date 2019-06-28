using Microsoft.Owin.Hosting;
using System;

namespace Sunday.WebApi.Host.Configuration
{
    public class WebApiManager
    {
        protected IDisposable WebApplication { get; set; }

        public bool Start()
        {
            Console.WriteLine("Starting Service...");

            if (WebApplication == null)
            {
                WebApplication = WebApp.Start
                (
                    new StartOptions
                    {
                        Port = 12340
                    },
                    appBuilder =>
                    {
                        new StartupConfig().Configure(appBuilder);
                    }
                );
            }

            Console.WriteLine("Service Started");
            return true;
        }

        public bool Stop()
        {
            Console.WriteLine("Stopping Service...");
            return true;
        }
    }
}
