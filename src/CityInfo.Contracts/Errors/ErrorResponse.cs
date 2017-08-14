using System;
using System.Collections.Generic;

namespace CityInfo.Contracts.Errors
{
    [Serializable]
    public class ErrorResponse
    {
        public string Reason { get; }
        public string Message { get; }
        public Dictionary<string, string> Params { get; }

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
