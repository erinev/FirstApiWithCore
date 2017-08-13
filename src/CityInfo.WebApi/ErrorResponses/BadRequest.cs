using System.Collections.Generic;
using CityInfo.Contracts.Constants;
using CityInfo.Contracts.Errors;

namespace CityInfo.WebApi.ErrorResponses
{
    public static class BadRequest
    {
        public static class PlaceToVisit
        {
            public static ErrorResponse BuildNameIsInvalidResponse(Contracts.WriteModel.PlaceToVisit placeToVisit)
            {
                return new ErrorResponse(
                    "PlaceToVisitNameIsInvalid",
                    "Provided placement name is invalid",
                    new Dictionary<string, string> {
                        { "value", placeToVisit.Name },
                        { "currentLength", placeToVisit.Name.Length.ToString() },
                        { "minimumLength", ValidationRules.PlaceToVisit.MinimumNameLength.ToString() },
                        { "maximumLength", ValidationRules.PlaceToVisit.MaximumNameLength.ToString() }

                    }
                );
            }

            public static ErrorResponse BuildResourceNotProvidedResponse()
            {
                return new ErrorResponse(
                    "PlaceToVisitIsNotProvided",
                    "Place to visit is not provided within request",
                    new Dictionary<string, string> {}
                );
            }
        }
    }
}
