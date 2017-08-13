using System.Collections.Generic;
using System.Linq;
using CityInfo.Contracts.Dtos;
using CityInfo.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.WebApi.Controllers
{
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        private static ICitiesRepository _citiesRepository;

        public CitiesController()
        {
            if (_citiesRepository == null)
            {
                _citiesRepository = new CitiesRepository();
            }
        }

        [HttpGet()]
        public ActionResult GetAll()
        {
            List<City> cities = _citiesRepository.GetAll();

            return Ok(cities);
        }

        [HttpGet("{cityId:int}")]
        public ActionResult GetById(int cityId)
        {
            City city = _citiesRepository.GetById(cityId);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }

        [HttpDelete("{cityId:int}")]
        public ActionResult DeleteById(int cityId)
        {
            City city = _citiesRepository.GetById(cityId);

            if (city == null)
            {
                return NotFound();
            }

            _citiesRepository.DeleteById(cityId);

            return NoContent();
        }

        [HttpGet("{cityId:int}/placesToVisit")]
        public ActionResult GetCitysPlacesToVisit(int cityId)
        {
            City city = _citiesRepository.GetById(cityId);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city.PlacesToVisit);
        }

        [HttpGet("{cityId:int}/placesToVisit/{placeToVisitId:int}")]
        public ActionResult GetCitysPlaceToVisitById(int cityId, int placeToVisitId)
        {
            City city = _citiesRepository.GetById(cityId);

            if (city is null)
            {
                return NotFound();
            }

            PlaceToVisit placeToVisit = city.PlacesToVisit.FirstOrDefault(place => place.Id == placeToVisitId);

            if (placeToVisit is null)
            {
                return NotFound();
            }

            return Ok(placeToVisit);
        }
    }
}
