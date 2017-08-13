using System.Collections.Generic;
using CityInfo.Contracts.Constants;
using CityInfo.Contracts.Errors;
using CityInfo.Contracts.WriteModel;

namespace CityInfo.WebApi.ErrorResponses
{
    public static class BadRequest
    {
        public static class PlaceToVisitErrors
        {
            public static ErrorResponse BuildPlaceToVisitNameIsInvalidResponse(PlaceToVisit placeToVisit)
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

            public static ErrorResponse BuildPlaceToVisitNotProvidedResponse()
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
