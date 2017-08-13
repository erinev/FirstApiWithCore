using System.Collections.Generic;
using System.Linq;
using CityInfo.Contracts.Dtos;

namespace CityInfo.WebApi.Repositories
{
    interface ICitiesRepository
    {
        List<City> GetAll();
        City GetById(int cityId);
        void DeleteById(int cityId);
    }

    public class CitiesRepository : ICitiesRepository
    {
        private List<City> _cities;

        public CitiesRepository()
        {
            _cities = BuildCitiesMock();
        }

        public List<City> GetAll()
        {
            return _cities;
        }

        public City GetById(int cityId)
        {
            City foundCity = _cities.Find(city => city.Id == cityId);

            return foundCity;
        }

        public void DeleteById(int cityId)
        {
            List<City> listWithoutRemovedCity = _cities.Where(city => city.Id != cityId).ToList();

            _cities = listWithoutRemovedCity;
        }

        #region Private Functions

        private static List<City> BuildCitiesMock()
        {
            var cities = new List<City>
            {
                new City
                {
                    Id = 1,
                    Name = "Birštonas",
                    Description = "City of dreams",
                    PlacesToVisit = new List<PlaceToVisit>
                    {
                        new PlaceToVisit
                        {
                            Id = 1,
                            Name = "Banginuko vaišės",
                            Description = "Deserts and snacks caffe",
                            Address = "Birutės g. 29, Birštonas LT-59217"
                        },
                        new PlaceToVisit
                        {
                            Id = 1,
                            Name = "Saulės terasa",
                            Description = "Deserts and snack caffe on the top of 9th floor building",
                            Address = "Algirdo g. 14, Birštonas LT-59204"
                        },
                    }
                },
                new City
                {
                    Id = 2,
                    Name = "Kaunas",
                    Description = "City of contruction",
                    PlacesToVisit = new List<PlaceToVisit>
                    {
                        new PlaceToVisit
                        {
                            Id = 1,
                            Name = "Pilies Sodas",
                            Description = "Cousy restaurant in the heart of Kaunas",
                            Address = "Pilies g. 12, Kaunas LT-44275"
                        }
                    }
                },
                new City
                {
                    Id = 3,
                    Name = "Vilnius",
                    Description = "City of wasted money",
                    PlacesToVisit = new List<PlaceToVisit>
                    {
                        new PlaceToVisit
                        {
                            Id = 1,
                            Name = "Cat Caffe",
                            Description = "Cousy restaurant with a lot of cats",
                            Address = "J. Jasinskio g. 1, Vilnius LT-01112"
                        },
                    }
                }
            };

            return cities;
        }

        #endregion
    }
}
