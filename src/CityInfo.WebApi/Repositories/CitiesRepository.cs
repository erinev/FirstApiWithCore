using System.Collections.Generic;
using CityInfo.Contracts.Dtos;

namespace CityInfo.WebApi.Repositories
{
    interface ICitiesRepository
    {
        List<City> GetCities();
    }

    public class CitiesRepository : ICitiesRepository
    {
        public List<City> GetCities()
        {
            var cities = new List<City>
            {
                new City
                {
                    Id = 1,
                    Name = "Birštonas",
                    Description = "City of dreams",
                    PointOfInterests = new List<PointOfInterest>
                    {
                        
                    }
                },
                new City
                {
                    Id = 2,
                    Name = "Kaunas",
                    Description = "City of contruction",
                    PointOfInterests = new List<PointOfInterest>
                    {

                    }
                },
                new City
                {
                    Id = 3,
                    Name = "Vilnius",
                    Description = "City of wasted money",
                    PointOfInterests = new List<PointOfInterest>
                    {

                    }
                }
            };

            return cities;
        }
    }
}
