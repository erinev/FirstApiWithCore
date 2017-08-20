using Microsoft.Extensions.Logging;

namespace CityInfo.Configuration.Logging.Log4Net
{
    public static class Log4NetExtensions
    {
        public static void AddLog4Net(this ILoggerFactory factory, string log4NetConfigFile)
        {
            factory.AddProvider(new Log4NetProvider(log4NetConfigFile));
        }
    }
}
