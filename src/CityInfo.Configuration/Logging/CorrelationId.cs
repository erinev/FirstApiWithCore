using System.Threading;

namespace CityInfo.Configuration.Logging
{
    public static class CorrelationId
    {
        private static readonly AsyncLocal<string> _current = new AsyncLocal<string>();
        public static string Current
        {
            get => _current.Value;
            set
            {
                _current.Value = value;
                log4net.LogicalThreadContext.Properties["CorrelationId"] = value;
            }
        }
    }
}
