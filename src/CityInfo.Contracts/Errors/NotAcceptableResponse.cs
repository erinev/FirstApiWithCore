using System.Collections.Generic;

namespace CityInfo.Contracts.Errors
{
    public class NotAcceptableResponse : ErrorResponse
    {
        public NotAcceptableResponse(string reason, string message, Dictionary<string, string> @params = null)
            : base(reason, message, @params)
        {
        }
    }
}
