using System;
using System.Collections.Generic;
using CityInfo.Configuration.Swagger.Examples;
using CityInfo.Contracts.Responses;
using CityInfo.Contracts.Requests;

namespace CityInfo.WebApi.Examples
{
    /// <summary>
    /// Swagger examples provider for CityInfo API
    /// </summary>
    public class ExamplesProvider : IExampleProvider
    {
        private static readonly IDictionary<Type, object> ExampleByType = new Dictionary<Type, object>
        {
            { typeof(CityDto), Examples.CityDtoExample },
            { typeof(PlaceToVisitDocument), Examples.PlaceToVisitDocumentExample },
            { typeof(PlaceToVisitRequest), Examples.PlaceToVisitRequestExample }
        };

        /// <summary>
        /// Gets example object by type
        /// </summary>
        /// <param name="key">Object type</param>
        /// <returns>Object</returns>
        public object Get(Type key)
        {
            object example;
            return ExampleByType.TryGetValue(key, out example) ? example : null;
        }

    }
}
