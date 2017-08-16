using System.Collections.Generic;
using CityInfo.Contracts.Readmodel;
using CityInfo.Contracts.WriteModel;

namespace CityInfo.WebApi.Examples
{
    internal static class Examples
    {
        internal static readonly CityDocument CityDocumentExample = new CityDocument
        {
            Id = 1,
            Name = "Nameless City",
            Description = "City where name doesn't matter",
            PlacesToVisit = new List<PlaceToVisitDocument>
            {
                new PlaceToVisitDocument
                {
                    Id = 1,
                    Name = "Nameless place",
                    Description = "Nameless description",
                    Address = "Nameless st. 20, Namelessia NM-55845"
                }
            }
        };

        internal static PlaceToVisitDocument PlaceToVisitDocumentExample = new PlaceToVisitDocument
        {
            Id = 1,
            Name = "Nameless place",
            Description = "Nameless description",
            Address = "Nameless st. 20, Namelessia NM-55845"
        };

        internal static PlaceToVisit PlaceToVisitExample = new PlaceToVisit
        {
            Name = "Nameless place",
            Description = "Nameless description",
            Address = "Nameless st. 20, Namelessia NM-55845"
        };
    }
}
