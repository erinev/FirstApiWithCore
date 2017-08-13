using System.Collections.Generic;
using System.Linq;
using CityInfo.Contracts.Constants;
using CityInfo.Contracts.Readmodel;
using CityInfo.Contracts.WriteModel;
using CityInfo.WebApi.Errors;
using CityInfo.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.WebApi.Controllers
{
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        private static ICitiesRepository _citiesRepository;
        private static readonly string GetCitysPlaceToVisitByIdRouteName = "GetCitysPlaceToVisitById";

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
            List<CityDocument> cities = _citiesRepository.GetAllCities();

            return Ok(cities);
        }

        [HttpGet("{cityId:int}")]
        public ActionResult GetById(int cityId)
        {
            CityDocument cityDocument = _citiesRepository.GetCityById(cityId);

            if (cityDocument == null)
            {
                return NotFound();
            }

            return Ok(cityDocument);
        }

        [HttpDelete("{cityId:int}")]
        public ActionResult DeleteById(int cityId)
        {
            CityDocument cityDocument = _citiesRepository.GetCityById(cityId);

            if (cityDocument == null)
            {
                return NotFound();
            }

            _citiesRepository.DeleteCityById(cityId);

            return NoContent();
        }

        [HttpGet("{cityId:int}/placesToVisit")]
        public ActionResult GetCitysPlacesToVisit(int cityId)
        {
            CityDocument cityDocument = _citiesRepository.GetCityById(cityId);

            if (cityDocument == null)
            {
                return NotFound();
            }

            return Ok(cityDocument.PlacesToVisit);
        }

        [HttpGet("{cityId:int}/placesToVisit/{placeToVisitId:int}", Name = "GetCitysPlaceToVisitById")]
        public ActionResult GetCitysPlaceToVisitById(int cityId, int placeToVisitId)
        {
            CityDocument cityDocument = _citiesRepository.GetCityById(cityId);

            if (cityDocument is null)
            {
                return NotFound();
            }

            PlaceToVisitDocument placeToVisitDocument = cityDocument.PlacesToVisit.FirstOrDefault(place => place.Id == placeToVisitId);

            if (placeToVisitDocument is null)
            {
                return NotFound();
            }

            return Ok(placeToVisitDocument);
        }

        [HttpPost("{cityId:int}/placesToVisit")]
        public ActionResult AddPlaceToVisitForCity(int cityId, [FromBody] PlaceToVisit newPlaceToVisit)
        {
            if (newPlaceToVisit == null)
            {
                return BadRequest(BadRequestErrors.PlaceToVisitErrors.BuildPlaceToVisitNotProvidedResponse());
            }

            if (newPlaceToVisit.Name.Length < ValidationRules.PlaceToVisit.MinimumNameLength ||
                newPlaceToVisit.Name.Length > ValidationRules.PlaceToVisit.MaximumNameLength)
            {
                return BadRequest(BadRequestErrors.PlaceToVisitErrors.BuildPlaceToVisitNameIsInvalidResponse(newPlaceToVisit));
            }

            CityDocument cityDocument = _citiesRepository.GetCityById(cityId);

            if (cityDocument == null)
            {
                return NotFound();
            }

            PlaceToVisitDocument addedPlaceToVisitDocument = _citiesRepository.AddPlaceToVisitForCity(cityId, newPlaceToVisit);

            return CreatedAtRoute(
                GetCitysPlaceToVisitByIdRouteName, 
                new { cityId, placeToVisitId = addedPlaceToVisitDocument.Id }, 
                addedPlaceToVisitDocument);
        }
    }
}
