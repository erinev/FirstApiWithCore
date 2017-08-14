using System.Collections.Generic;
using CityInfo.Contracts.Errors;

namespace CityInfo.WebApi.ErrorResponses
{
    /// <summary>
    /// Container of application's not found error responses
    /// </summary>
    public static class NotFound
    {
        /// <summary>
        /// Container of city's not found responses
        /// </summary>
        public static class City
        {
            /// <summary>
            /// Builds CityNotFound not found response
            /// </summary>
            /// <param name="cityId"></param>
            /// <returns>Error response</returns>
            public static ErrorResponse BuildResponse(int cityId)
            {
                return new ErrorResponse(
                    "CityNotFound",
                    "Requested city is not found",
                    new Dictionary<string, string> {
                        { "cityId", cityId.ToString() }
                    }
                );
            }
        }

        /// <summary>
        /// Container of city's place to visit not found responses
        /// </summary>
        public static class PlaceToVisit
        {
            /// <summary>
            /// Builds BuildPlaceToVisitNotFoundResponse not found response
            /// </summary>
            /// <param name="cityId">Unique identifier for city</param>
            /// <param name="placeToVisitId">Unique identifier for city's place to visit</param>
            /// <returns>Error response</returns>
            public static ErrorResponse BuildResponse(int cityId, int placeToVisitId)
            {
                return new ErrorResponse(
                    "PlaceToVisitNotFound",
                    "Requested place to visit is not found in city",
                    new Dictionary<string, string> {
                        { "cityId", cityId.ToString() },
                        { "placeToVisitId", placeToVisitId.ToString() }

                    }
                );
            }
        }
    }
}
