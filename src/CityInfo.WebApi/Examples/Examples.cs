using System.Collections.Generic;
using CityInfo.Contracts.Requests;
using CityInfo.Contracts.Responses;

namespace CityInfo.WebApi.Examples
{
    internal static class Examples
    {
        internal static readonly CityDto CityDtoExample = new CityDto
        {
            Id = 1,
            Name = "Nameless City",
            Description = "City where name doesn't matter",
            PlacesToVisit = new List<PlaceToVisitDto>
            {
                new PlaceToVisitDto
                {
                    Id = 1,
                    Name = "Nameless place",
                    Description = "Nameless description",
                    Address = "Nameless st. 20, Namelessia NM-55845"
                }
            }
        };

        internal static readonly PlaceToVisitDto PlaceToVisitDtoExample = new PlaceToVisitDto
        {
            Id = 1,
            Name = "Nameless place",
            Description = "Nameless description",
            Address = "Nameless st. 20, Namelessia NM-55845"
        };

        internal static readonly PlaceToVisitRequest PlaceToVisitRequestExample = new PlaceToVisitRequest
        {
            Name = "Nameless place",
            Description = "Nameless description",
            Address = "Nameless st. 20, Namelessia NM-55845"
        };
    }
}
