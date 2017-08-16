using System.Collections.Generic;
using CityInfo.Contracts.Constants;
using CityInfo.Contracts.Errors;

namespace CityInfo.WebApi.ErrorResponses
{
    /// <summary>
    /// Container of application's bad request error responses
    /// </summary>
    public static class BadRequest
    {
        /// <summary>
        /// Container of place to visit bad request responses
        /// </summary>
        public static class PlaceToVisit
        {
            /// <summary>
            /// Builds PlaceToVisitIsNotProvided bad request response
            /// </summary>
            /// <returns>Bad request error response</returns>
            public static BadRequestResponse BuildResourceNotProvidedResponse()
            {
                return new BadRequestResponse(
                    "PlaceToVisitIsNotProvided",
                    "Place to visit resource is not provided within request",
                    new Dictionary<string, string>()
                );
            }

            /// <summary>
            /// Builds PlaceToVisitNameIsInvalid bad request response
            /// </summary>
            /// <param name="newPlaceToVisit">new place to visit resource</param>
            /// <returns>Bad request error response</returns>
            public static BadRequestResponse BuildNameIsInvalidResponse(Contracts.WriteModel.PlaceToVisit newPlaceToVisit)
            {
                return new BadRequestResponse(
                    "PlaceToVisitNameIsInvalid",
                    "Provided place to visit name is too short or too long",
                    new Dictionary<string, string> {
                        { "value", newPlaceToVisit.Name },
                        { "currentLength", newPlaceToVisit.Name.Length.ToString() },
                        { "minimumLength", ValidationRules.PlaceToVisit.MinimumNameLength.ToString() },
                        { "maximumLength", ValidationRules.PlaceToVisit.MaximumNameLength.ToString() }

                    }
                );
            }

            /// <summary>
            /// Builds PlaceToVisitDescriptionIsInvalid bad request response
            /// </summary>
            /// <param name="newPlaceToVisit">new place to visit resource</param>
            /// <returns>Bad request error response</returns>
            public static BadRequestResponse BuildDescriptionIsInvalidResponse(Contracts.WriteModel.PlaceToVisit newPlaceToVisit)
            {
                return new BadRequestResponse(
                    "PlaceToVisitDescriptionIsInvalid",
                    "Provided place to visit description is too long",
                    new Dictionary<string, string> {
                        { "value", newPlaceToVisit.Description },
                        { "currentLength", newPlaceToVisit.Description.Length.ToString() },
                        { "maximumLength", ValidationRules.PlaceToVisit.MaximumDescriptionLength.ToString() }

                    }
                );
            }

            /// <summary>
            /// Builds PlaceToVisitAddressIsInvalid bad request response
            /// </summary>
            /// <param name="newPlaceToVisit">new place to visit resource</param>
            /// <returns>Bad request error response</returns>
            public static BadRequestResponse BuildAddressIsInvalidResponse(Contracts.WriteModel.PlaceToVisit newPlaceToVisit)
            {
                return new BadRequestResponse(
                    "PlaceToVisitAddressIsInvalid",
                    "Provided place to visit address is too short or too long",
                    new Dictionary<string, string> {
                        { "value", newPlaceToVisit.Address },
                        { "currentLength", newPlaceToVisit.Address.Length.ToString() },
                        { "minimumLength", ValidationRules.PlaceToVisit.MininumAddressLength.ToString() },
                        { "maximumLength", ValidationRules.PlaceToVisit.MaximumAddressLength.ToString() }

                    }
                );
            }
        }
    }
}
