using System.Collections.Generic;
using CityInfo.Contracts.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.WebApi.Controllers
{
    public class CitiesController : Controller
    {
        [HttpGet("api/cities")]
        public ActionResult GetCities()
        {
            var cities = new List<City>
            {
                new City { Id = 1, Name = "Birštonas", Description = "City of dreams" },
                new City { Id = 2, Name = "Kaunas", Description = "City of contruction" },
                new City { Id = 3, Name = "Vilnius", Description = "City of wasted money" }

            };

            return Ok(cities);
        }
    }
}
