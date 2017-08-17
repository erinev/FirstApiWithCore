using System.Collections.Generic;

namespace CityInfo.Contracts.Errors
{
    public class InternalServerErrorResponse : ErrorResponse
    {
        public InternalServerErrorResponse(string reason, string message, Dictionary<string, string> @params)
            : base(reason, message, @params)
        {
        }
    }
}