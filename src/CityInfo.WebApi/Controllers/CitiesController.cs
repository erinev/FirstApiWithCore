using System.Collections.Generic;
using System.Linq;
using CityInfo.Contracts.Constants;
using CityInfo.Contracts.Readmodel;
using CityInfo.Contracts.WriteModel;
using CityInfo.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CityInfo.WebApi.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Resource for managing city information
    /// </summary>
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        private static ICitiesRepository _citiesRepository;
        private const string GetCitysPlaceToVisitByIdRouteName = "GetCitysPlaceToVisitById";

        /// <inheritdoc />
        public CitiesController()
        {
            if (_citiesRepository == null)
            {
                _citiesRepository = new CitiesRepository();
            }
        }

        /// <summary>
        /// Returns all cities
        /// </summary>
        /// <returns>List of cities</returns>
        [HttpGet()]
        [SwaggerResponse(200, typeof(List<CityDocument>))]
        public ActionResult GetAll()
        {
            List<CityDocument> cities = _citiesRepository.GetAllCities();

            return Ok(cities);
        }

        /// <summary>
        /// Returns single city by id
        /// </summary>
        /// <param name="cityId">Unique identifier for city</param>
        /// <returns>Single city</returns>
        [HttpGet("{cityId:int}")]
        [SwaggerResponse(200, typeof(CityDocument))]
        public ActionResult GetById(int cityId)
        {
            CityDocument cityDocument = _citiesRepository.GetCityById(cityId);

            if (cityDocument == null)
            {
                return NotFound(ErrorResponses.NotFound.City.BuildResponse(cityId));
            }

            return Ok(cityDocument);
        }

        /// <summary>
        /// Removes city by id
        /// </summary>
        /// <param name="cityId">Unique identifier for city</param>
        /// <returns></returns>
        [HttpDelete("{cityId:int}")]
        [SwaggerResponse(204)]
        public ActionResult DeleteById(int cityId)
        {
            CityDocument cityDocument = _citiesRepository.GetCityById(cityId);

            if (cityDocument == null)
            {
                return NotFound(ErrorResponses.NotFound.City.BuildResponse(cityId));
            }

            _citiesRepository.DeleteCityById(cityId);

            return NoContent();
        }

        /// <summary>
        /// Get list of places to visit in city
        /// </summary>
        /// <param name="cityId">Unique identifier for city</param>
        /// <returns>Places to visit</returns>
        [HttpGet("{cityId:int}/placesToVisit")]
        [SwaggerResponse(200, typeof(List<PlaceToVisitDocument>))]
        public ActionResult GetCitysPlacesToVisit(int cityId)
        {
            CityDocument cityDocument = _citiesRepository.GetCityById(cityId);

            if (cityDocument == null)
            {
                return NotFound(ErrorResponses.NotFound.City.BuildResponse(cityId));
            }

            return Ok(cityDocument.PlacesToVisit);
        }

        /// <summary>
        /// Get city's place to visit by id
        /// </summary>
        /// <param name="cityId">Unique identifier for city</param>
        /// <param name="placeToVisitId">Unique identifier for city's place to visit</param>
        /// <returns>Single place to visit</returns>
        [HttpGet("{cityId:int}/placesToVisit/{placeToVisitId:int}", Name = GetCitysPlaceToVisitByIdRouteName)]
        [SwaggerResponse(200, typeof(PlaceToVisitDocument))]
        public ActionResult GetCitysPlaceToVisitById(int cityId, int placeToVisitId)
        {
            CityDocument cityDocument = _citiesRepository.GetCityById(cityId);

            if (cityDocument is null)
            {
                return NotFound(ErrorResponses.NotFound.City.BuildResponse(cityId));
            }

            PlaceToVisitDocument placeToVisitDocument = cityDocument.PlacesToVisit.FirstOrDefault(place => place.Id == placeToVisitId);

            if (placeToVisitDocument is null)
            {
                return NotFound(ErrorResponses.NotFound.PlaceToVisit.BuildResponse(cityId, placeToVisitId));
            }

            return Ok(placeToVisitDocument);
        }

        /// <summary>
        /// Adds new place to visit for city
        /// </summary>
        /// <param name="cityId">Unique identifier for city</param>
        /// <param name="newPlaceToVisit">New place to visit</param>
        /// <returns>Newly created place to visit</returns>
        [HttpPost("{cityId:int}/placesToVisit")]
        [SwaggerResponse(201, typeof(PlaceToVisitDocument))]
        public ActionResult AddPlaceToVisitForCity(int cityId, [FromBody] PlaceToVisit newPlaceToVisit)
        {
            if (newPlaceToVisit == null)
            {
                return BadRequest(ErrorResponses.BadRequest.PlaceToVisit.BuildResourceNotProvidedResponse());
            }

            if (newPlaceToVisit.Name == null ||
                newPlaceToVisit.Name.Length < ValidationRules.PlaceToVisit.MinimumNameLength ||
                newPlaceToVisit.Name.Length > ValidationRules.PlaceToVisit.MaximumNameLength)
            {
                return BadRequest(ErrorResponses.BadRequest.PlaceToVisit.BuildNameIsInvalidResponse(newPlaceToVisit));
            }

            if (newPlaceToVisit.Description == null ||
                newPlaceToVisit.Description.Length > ValidationRules.PlaceToVisit.MaximumDescriptionLength)
            {
                return BadRequest(ErrorResponses.BadRequest.PlaceToVisit.BuildDescriptionIsInvalidResponse(newPlaceToVisit));
            }

            if (newPlaceToVisit.Address == null ||
                newPlaceToVisit.Address.Length < ValidationRules.PlaceToVisit.MininumAddressLength ||
                newPlaceToVisit.Address.Length > ValidationRules.PlaceToVisit.MaximumAddressLength)
            {
                return BadRequest(ErrorResponses.BadRequest.PlaceToVisit.BuildAddressIsInvalidResponse(newPlaceToVisit));
            }

            CityDocument cityDocument = _citiesRepository.GetCityById(cityId);

            if (cityDocument == null)
            {
                return NotFound(ErrorResponses.NotFound.City.BuildResponse(cityId));
            }

            PlaceToVisitDocument addedPlaceToVisitDocument = _citiesRepository.AddPlaceToVisitForCity(cityId, newPlaceToVisit);

            return CreatedAtRoute(
                GetCitysPlaceToVisitByIdRouteName, 
                new { cityId, placeToVisitId = addedPlaceToVisitDocument.Id }, 
                addedPlaceToVisitDocument);
        }
    }
}
