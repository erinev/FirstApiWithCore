﻿using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace CityInfo.API
{
    public static class Program
    {
        public static void Main()
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }
    }
}
