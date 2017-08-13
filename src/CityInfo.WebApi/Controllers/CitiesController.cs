using System.Collections.Generic;
using CityInfo.Contracts.Dtos;
using CityInfo.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.WebApi.Controllers
{
    public class CitiesController : Controller
    {
        private readonly ICitiesRepository _citiesRepository;

        public CitiesController()
        {
            _citiesRepository = new CitiesRepository();
        }

        [HttpGet("api/cities")]
        public ActionResult GetCities()
        {
            List<City> cities = _citiesRepository.GetCities();

            return Ok(cities);
        }
    }
}
