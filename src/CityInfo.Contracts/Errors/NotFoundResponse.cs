using System.Collections.Generic;

namespace CityInfo.Contracts.Errors
{
    public class NotFoundResponse : ErrorResponse
    {
        public NotFoundResponse(string reason, string message, Dictionary<string, string> @params)
            : base(reason, message, @params)
        {
        }
    }
}