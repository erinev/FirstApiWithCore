using System.Collections.Generic;
using System.Linq;
using CityInfo.Contracts.Readmodel;
using CityInfo.Contracts.WriteModel;

namespace CityInfo.WebApi.Repositories
{
    interface ICitiesRepository
    {
        List<CityDocument> GetAllCities();
        CityDocument GetCityById(int cityId);
        void DeleteCityById(int cityId);

        PlaceToVisitDocument AddPlaceToVisitForCity(int cityId, PlaceToVisit newPlaceToVisit);
    }

    public class CitiesRepository : ICitiesRepository
    {
        private List<CityDocument> _cities;

        public CitiesRepository()
        {
            _cities = BuildCitiesMock();
        }

        public List<CityDocument> GetAllCities()
        {
            return _cities;
        }

        public CityDocument GetCityById(int cityId)
        {
            CityDocument cityDocument = _cities.Find(city => city.Id == cityId);

            return cityDocument;
        }

        public void DeleteCityById(int cityId)
        {
            List<CityDocument> listWithoutRemovedCity = _cities.Where(city => city.Id != cityId).ToList();

            _cities = listWithoutRemovedCity;
        }

        public PlaceToVisitDocument AddPlaceToVisitForCity(int cityId, PlaceToVisit newPlaceToVisit)
        {
            CityDocument cityDocument = _cities.Find(city => city.Id == cityId);

            var newPlaceToVisitDocument = new PlaceToVisitDocument
            {
                Id = cityDocument.NumberOfPlacesToVisit + 1,
                Name = newPlaceToVisit.Name,
                Description = newPlaceToVisit.Description,
                Address = newPlaceToVisit.Address
            };

            cityDocument.PlacesToVisit.Add(newPlaceToVisitDocument);

            return newPlaceToVisitDocument;
        }

        #region Private Functions

        private static List<CityDocument> BuildCitiesMock()
        {
            var cities = new List<CityDocument>
            {
                new CityDocument
                {
                    Id = 1,
                    Name = "Birštonas",
                    Description = "City of dreams",
                    PlacesToVisit = new List<PlaceToVisitDocument>
                    {
                        new PlaceToVisitDocument
                        {
                            Id = 1,
                            Name = "Banginuko vaišės",
                            Description = "Deserts and snacks caffe",
                            Address = "Birutės g. 29, Birštonas LT-59217"
                        },
                        new PlaceToVisitDocument
                        {
                            Id = 2,
                            Name = "Saulės terasa",
                            Description = "Deserts and snack caffe on the top of 9th floor building",
                            Address = "Algirdo g. 14, Birštonas LT-59204"
                        },
                    }
                },
                new CityDocument
                {
                    Id = 2,
                    Name = "Kaunas",
                    Description = "City of contruction",
                    PlacesToVisit = new List<PlaceToVisitDocument>
                    {
                        new PlaceToVisitDocument
                        {
                            Id = 1,
                            Name = "Pilies Sodas",
                            Description = "Cousy restaurant in the heart of Kaunas",
                            Address = "Pilies g. 12, Kaunas LT-44275"
                        }
                    }
                },
                new CityDocument
                {
                    Id = 3,
                    Name = "Vilnius",
                    Description = "City of wasted money",
                    PlacesToVisit = new List<PlaceToVisitDocument>
                    {
                        new PlaceToVisitDocument
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
