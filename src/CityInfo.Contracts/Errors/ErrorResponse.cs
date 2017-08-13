using System.Collections.Generic;

namespace CityInfo.Contracts.Errors
{
    public class ErrorResponse
    {
        private string Reason { get; }
        private string Message { get; }
        private Dictionary<string, string> Params { get; }

        public ErrorResponse(string reason, string message) : this(reason, message, null)
        {
        }

        public ErrorResponse(string reason, string message, Dictionary<string, string> @params)
        {
            Reason = reason;
            Message = message;
            Params = @params;
        }
    }
}
