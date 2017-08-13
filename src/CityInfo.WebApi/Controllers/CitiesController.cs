using System.Collections.Generic;
using CityInfo.Contracts.Dtos;
using CityInfo.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.WebApi.Controllers
{
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        private readonly ICitiesRepository _citiesRepository;

        public CitiesController()
        {
            _citiesRepository = new CitiesRepository();
        }

        [HttpGet()]
        public ActionResult GetAll()
        {
            List<City> cities = _citiesRepository.GetAll();

            return Ok(cities);
        }

        [HttpGet("{id:int}")]
        public ActionResult GetCities(int id)
        {
            City city = _citiesRepository.GetById(id);

            return Ok(city);
        }
    }
}
