using System.Collections.Generic;

namespace CityInfo.Contracts.Errors
{
    public class ForbiddenResponse : ErrorResponse
    {
        public ForbiddenResponse(string reason, string message, Dictionary<string, string> @params)
            : base(reason, message, @params)
        {
        }
    }
}