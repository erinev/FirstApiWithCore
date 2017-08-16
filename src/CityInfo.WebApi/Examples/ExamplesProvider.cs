using System;
using System.Collections.Generic;
using CityInfo.Configuration.Swagger.Examples;
using CityInfo.Contracts.Readmodel;
using CityInfo.Contracts.WriteModel;

namespace CityInfo.WebApi.Examples
{
    /// <summary>
    /// Swagger examples provider for CityInfo API
    /// </summary>
    public class ExamplesProvider : IExampleProvider
    {
        private static readonly IDictionary<Type, object> ExampleByType = new Dictionary<Type, object>
        {
            { typeof(CityDocument), Examples.CityDocumentExample },
            { typeof(PlaceToVisitDocument), Examples.PlaceToVisitDocumentExample },
            { typeof(PlaceToVisit), Examples.PlaceToVisitExample }
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
