using System.Threading;

namespace CityInfo.Configuration.Logging
{
    public static class CorrelationId
    {
        private static readonly AsyncLocal<string> _current = new AsyncLocal<string>();
        public static string Current
        {
            get
            {
                return _current.Value;
            }
            set
            {
                _current.Value = value;
                // TODO: log4net.LogicalThreadContext.Properties["CorrelationId"] = value;
            }
        }
    }
}
