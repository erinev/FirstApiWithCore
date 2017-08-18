using System.Collections.Generic;
using System.Linq;
using CityInfo.Contracts.Responses;
using CityInfo.Contracts.Requests;

namespace CityInfo.WebApi.Repositories
{
    interface ICitiesRepository
    {
        List<CityDto> GetAllCities();
        CityDto GetCityById(int cityId);
        void DeleteCityById(int cityId);

        PlaceToVisitDto AddPlaceToVisitForCity(int cityId, PlaceToVisitRequest newPlaceToVisitRequest);
    }

    /// <summary>
    /// Provides set of methods to work with cities
    /// </summary>
    public class CitiesRepository : ICitiesRepository
    {
        private List<CityDto> _cities;

        /// <summary>
        /// CitiesRepository constructor
        /// </summary>
        public CitiesRepository()
        {
            _cities = BuildCitiesMock();
        }

        /// <summary>
        /// Returns all cities
        /// </summary>
        /// <returns>List of cities</returns>
        public List<CityDto> GetAllCities()
        {
            return _cities;
        }

        /// <summary>
        /// Returns city by id
        /// </summary>
        /// <param name="cityId">Unique identifier for city</param>
        /// <returns>Single city</returns>
        public CityDto GetCityById(int cityId)
        {
            CityDto cityDto = _cities.Find(city => city.Id == cityId);

            return cityDto;
        }

        /// <summary>
        /// Deletes city by id
        /// </summary>
        /// <param name="cityId">Unique identifier for city</param>
        public void DeleteCityById(int cityId)
        {
            List<CityDto> listWithoutRemovedCity = _cities.Where(city => city.Id != cityId).ToList();

            _cities = listWithoutRemovedCity;
        }

        /// <summary>
        /// Adds new place to visit for city
        /// </summary>
        /// <param name="cityId">Unique identifier for city</param>
        /// <param name="newPlaceToVisitRequest">New place to visit</param>
        /// <returns>Newly created place to visit</returns>
        public PlaceToVisitDto AddPlaceToVisitForCity(int cityId, PlaceToVisitRequest newPlaceToVisitRequest)
        {
            CityDto cityDto = _cities.Find(city => city.Id == cityId);

            var newPlaceToVisitDocument = new PlaceToVisitDto
            {
                Id = cityDto.NumberOfPlacesToVisit + 1,
                Name = newPlaceToVisitRequest.Name,
                Description = newPlaceToVisitRequest.Description,
                Address = newPlaceToVisitRequest.Address
            };

            cityDto.PlacesToVisit.Add(newPlaceToVisitDocument);

            return newPlaceToVisitDocument;
        }

        #region Private Functions

        private static List<CityDto> BuildCitiesMock()
        {
            var cities = new List<CityDto>
            {
                new CityDto
                {
                    Id = 1,
                    Name = "Birštonas",
                    Description = "City of dreams",
                    PlacesToVisit = new List<PlaceToVisitDto>
                    {
                        new PlaceToVisitDto
                        {
                            Id = 1,
                            Name = "Banginuko vaišės",
                            Description = "Deserts and snacks caffe",
                            Address = "Birutės g. 29, Birštonas LT-59217"
                        },
                        new PlaceToVisitDto
                        {
                            Id = 2,
                            Name = "Saulės terasa",
                            Description = "Deserts and snack caffe on the top of 9th floor building",
                            Address = "Algirdo g. 14, Birštonas LT-59204"
                        },
                    }
                },
                new CityDto
                {
                    Id = 2,
                    Name = "Kaunas",
                    Description = "City of contruction",
                    PlacesToVisit = new List<PlaceToVisitDto>
                    {
                        new PlaceToVisitDto
                        {
                            Id = 1,
                            Name = "Pilies Sodas",
                            Description = "Cousy restaurant in the heart of Kaunas",
                            Address = "Pilies g. 12, Kaunas LT-44275"
                        }
                    }
                },
                new CityDto
                {
                    Id = 3,
                    Name = "Vilnius",
                    Description = "City of wasted money",
                    PlacesToVisit = new List<PlaceToVisitDto>
                    {
                        new PlaceToVisitDto
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
