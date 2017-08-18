using System.Collections.Generic;
using System.Linq;
using CityInfo.Configuration.Swagger.Response.Attributes;
using CityInfo.Contracts.Constants;
using CityInfo.Contracts.Responses;
using CityInfo.Contracts.Requests;
using CityInfo.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CityInfo.WebApi.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Resource for managing city information
    /// </summary>
    [Route("api/v1/cities")]
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
        [SwaggerResponse(200, typeof(List<CityDto>))]
        public ActionResult GetAll()
        {
            List<CityDto> cities = _citiesRepository.GetAllCities();

            return Ok(cities);
        }

        /// <summary>
        /// Returns single city by id
        /// </summary>
        /// <param name="cityId">Unique identifier for city</param>
        /// <returns>Single city</returns>
        [HttpGet("{cityId:int}")]
        [SwaggerResponse(200, typeof(CityDto))]
        [SwaggerNotFoundResponse]
        public ActionResult GetById(int cityId)
        {
            CityDto cityDto = _citiesRepository.GetCityById(cityId);

            if (cityDto == null)
            {
                return NotFound(ErrorResponses.NotFound.City.BuildResponse(cityId));
            }

            return Ok(cityDto);
        }

        /// <summary>
        /// Removes city by id
        /// </summary>
        /// <param name="cityId">Unique identifier for city</param>
        /// <returns></returns>
        [HttpDelete("{cityId:int}")]
        [SwaggerResponse(204)]
        [SwaggerNotFoundResponse]
        public ActionResult DeleteById(int cityId)
        {
            CityDto cityDto = _citiesRepository.GetCityById(cityId);

            if (cityDto == null)
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
        [SwaggerResponse(200, typeof(List<PlaceToVisitDto>))]
        [SwaggerNotFoundResponse]
        public ActionResult GetCitysPlacesToVisit(int cityId)
        {
            CityDto cityDto = _citiesRepository.GetCityById(cityId);

            if (cityDto == null)
            {
                return NotFound(ErrorResponses.NotFound.City.BuildResponse(cityId));
            }

            return Ok(cityDto.PlacesToVisit);
        }

        /// <summary>
        /// Get city's place to visit by id
        /// </summary>
        /// <param name="cityId">Unique identifier for city</param>
        /// <param name="placeToVisitId">Unique identifier for city's place to visit</param>
        /// <returns>Single place to visit</returns>
        [HttpGet("{cityId:int}/placesToVisit/{placeToVisitId:int}", Name = GetCitysPlaceToVisitByIdRouteName)]
        [SwaggerResponse(200, typeof(PlaceToVisitDto))]
        [SwaggerNotFoundResponse]
        public ActionResult GetCitysPlaceToVisitById(int cityId, int placeToVisitId)
        {
            CityDto cityDto = _citiesRepository.GetCityById(cityId);

            if (cityDto == null)
            {
                return NotFound(ErrorResponses.NotFound.City.BuildResponse(cityId));
            }

            PlaceToVisitDto placeToVisitDto = cityDto.PlacesToVisit.FirstOrDefault(place => place.Id == placeToVisitId);

            if (placeToVisitDto == null)
            {
                return NotFound(ErrorResponses.NotFound.PlaceToVisit.BuildResponse(cityId, placeToVisitId));
            }

            return Ok(placeToVisitDto);
        }

        /// <summary>
        /// Adds new place to visit for city
        /// </summary>
        /// <param name="cityId">Unique identifier for city</param>
        /// <param name="newPlaceToVisitRequest">New place to visit</param>
        /// <returns>Newly created place to visit</returns>
        [HttpPost("{cityId:int}/placesToVisit")]
        [SwaggerResponse(201, typeof(PlaceToVisitDto))]
        [SwaggerNotFoundResponse]
        [SwaggerBadRequestResponse]
        public ActionResult AddPlaceToVisitForCity(int cityId, [FromBody] PlaceToVisitRequest newPlaceToVisitRequest)
        {
            if (newPlaceToVisitRequest == null)
            {
                return BadRequest(ErrorResponses.BadRequest.PlaceToVisit.BuildResourceNotProvidedResponse());
            }

            if (newPlaceToVisitRequest.Name == null ||
                string.IsNullOrWhiteSpace(newPlaceToVisitRequest.Name) ||
                newPlaceToVisitRequest.Name.Length < ValidationRules.PlaceToVisit.MinimumNameLength ||
                newPlaceToVisitRequest.Name.Length > ValidationRules.PlaceToVisit.MaximumNameLength)
            {
                return BadRequest(ErrorResponses.BadRequest.PlaceToVisit.BuildNameIsInvalidResponse(newPlaceToVisitRequest));
            }

            if (newPlaceToVisitRequest.Description == null ||
                string.IsNullOrWhiteSpace(newPlaceToVisitRequest.Description) ||
                newPlaceToVisitRequest.Description.Length > ValidationRules.PlaceToVisit.MaximumDescriptionLength)
            {
                return BadRequest(ErrorResponses.BadRequest.PlaceToVisit.BuildDescriptionIsInvalidResponse(newPlaceToVisitRequest));
            }

            if (newPlaceToVisitRequest.Address == null ||
                string.IsNullOrWhiteSpace(newPlaceToVisitRequest.Address) ||
                newPlaceToVisitRequest.Address.Length < ValidationRules.PlaceToVisit.MininumAddressLength ||
                newPlaceToVisitRequest.Address.Length > ValidationRules.PlaceToVisit.MaximumAddressLength)
            {
                return BadRequest(ErrorResponses.BadRequest.PlaceToVisit.BuildAddressIsInvalidResponse(newPlaceToVisitRequest));
            }

            CityDto cityDto = _citiesRepository.GetCityById(cityId);

            if (cityDto == null)
            {
                return NotFound(ErrorResponses.NotFound.City.BuildResponse(cityId));
            }

            PlaceToVisitDto addedPlaceToVisitDto = _citiesRepository.AddPlaceToVisitForCity(cityId, newPlaceToVisitRequest);

            return CreatedAtRoute(
                GetCitysPlaceToVisitByIdRouteName, 
                new { cityId, placeToVisitId = addedPlaceToVisitDto.Id }, 
                addedPlaceToVisitDto);
        }
    }
}
