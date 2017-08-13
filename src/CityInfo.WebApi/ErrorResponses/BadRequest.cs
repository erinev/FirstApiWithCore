using System.Collections.Generic;
using CityInfo.Contracts.Constants;
using CityInfo.Contracts.Errors;

namespace CityInfo.WebApi.ErrorResponses
{
    /// <summary>
    /// Container of application bad request error responses
    /// </summary>
    public static class BadRequest
    {
        /// <summary>
        /// Container of place to visit error responses
        /// </summary>
        public static class PlaceToVisit
        {
            /// <summary>
            /// Builds PlaceToVisitNameIsInvalid bad request response
            /// </summary>
            /// <param name="placeToVisit">Place to visit write model</param>
            /// <returns>Error response</returns>
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

            /// <summary>
            /// Builds PlaceToVisitIsNotProvided bad request response
            /// </summary>
            /// <returns>Error response</returns>
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
