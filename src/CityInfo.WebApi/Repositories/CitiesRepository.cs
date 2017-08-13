using System.Collections.Generic;
using CityInfo.Contracts.Dtos;

namespace CityInfo.WebApi.Repositories
{
    interface ICitiesRepository
    {
        List<City> GetAll();
        City GetById(int id);
    }

    public class CitiesRepository : ICitiesRepository
    {
        public List<City> GetAll()
        {
            List<City> cities = BuildCitiesMock();

            return cities;
        }

        public City GetById(int id)
        {
            List<City> cities = BuildCitiesMock();

            City foundCity = cities.Find(city => city.Id == id);

            return foundCity;
        }

        private static List<City> BuildCitiesMock()
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
