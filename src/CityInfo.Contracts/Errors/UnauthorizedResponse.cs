using System.Collections.Generic;

namespace CityInfo.Contracts.Errors
{
    public class UnauthorizedResponse : ErrorResponse
    {
        public UnauthorizedResponse(string reason, string message, Dictionary<string, string> @params)
            : base(reason, message, @params)
        {
        }
    }
}