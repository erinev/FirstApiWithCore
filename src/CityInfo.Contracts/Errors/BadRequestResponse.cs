using System.Collections.Generic;

namespace CityInfo.Contracts.Errors
{
    public class BadRequestResponse : ErrorResponse
    {
        public BadRequestResponse(string reason, string message, Dictionary<string, string> @params = null)
            : base(reason, message, @params)
        {
        }
    }
}