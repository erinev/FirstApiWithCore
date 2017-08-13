using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace CityInfo.WebApi
{
    /// <summary>
    /// Main application class
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Entry point of application
        /// </summary>
        /// <param name="args">Command line arguments</param>
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        private static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
