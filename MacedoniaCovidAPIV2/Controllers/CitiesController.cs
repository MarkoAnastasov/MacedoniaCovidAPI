using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MacedoniaCovidAPIV2.Models;
using MacedoniaCovidAPIV2.Interfaces;
using MacedoniaCovidAPIV2.Errors;

namespace MacedoniaCovidAPIV2.Controllers
{
    [Route("api/cities")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICitiesService _citiesService;

        public CitiesController(ICitiesService citiesService)
        {
            _citiesService = citiesService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Cities>> GetAll()
        {
            var errorMessage = new ValidationError();
            errorMessage.Value = "";
            var cities = new List<Cities>();
            try
            {
                cities = _citiesService.GetAllCities();
            }
            catch(Exception)
            {
                errorMessage.Value = "Проблем со апликацијата.Пробајте повторно!";
            }

            if(errorMessage.Value != "")
            {
                return NotFound(errorMessage);
            }

            return Ok(cities);
        }

        [HttpGet("{city}")]
        public ActionResult<Cities> GetCityByName(string city)
        {
            var errorMessage = new ValidationError();
            errorMessage.Value = "";
            var getCity = new Cities();
            try
            {
                getCity = _citiesService.GetCityByName(city,getCity,errorMessage);
            }
            catch (Exception)
            {
                errorMessage.Value = "Проблем со апликацијата.Пробајте повторно!";
            }

            if (errorMessage.Value != "")
            {
                return NotFound(errorMessage);
            }

            return Ok(getCity);
        }

        [HttpPost]
        public ActionResult<Cities> AddCity(Cities city)
        {
            var errorMessage = new ValidationError();
            errorMessage.Value = "";
            var createdCity = new Cities();
            try
            {
                createdCity = _citiesService.AddCity(city,errorMessage);
            }
            catch (Exception)
            {
                errorMessage.Value = "Проблем со апликацијата.Пробајте повторно!";
            }

            if (errorMessage.Value != "")
            {
                return BadRequest(errorMessage);
            }


            return CreatedAtAction("AddCity", createdCity);
        }

        [HttpPut]
        public ActionResult<Cities> UpdateCity(Cities city)
        {
            var errorMessage = new ValidationError();
            errorMessage.Value = "";
            var updatedCity = new Cities();
            try
            {
                updatedCity = _citiesService.UpdateCity(city, errorMessage);
            }
            catch (Exception)
            {
                errorMessage.Value = "Проблем со апликацијата.Пробајте повторно!";
            }

            if (errorMessage.Value != "")
            {
                return BadRequest(errorMessage);
            }

            return Ok(updatedCity);
        }

        [HttpDelete("{city}")]
        public ActionResult DeleteCity(string city)
        {
            var errorMessage = new ValidationError();
            errorMessage.Value = "";
            try
            {
                _citiesService.DeleteCity(city, errorMessage);
            }
            catch (Exception)
            {
                errorMessage.Value = "Проблем со апликацијата.Пробајте повторно!";
            }

            if (errorMessage.Value != "")
            {
                return BadRequest(errorMessage);
            }

            return Ok();
        }
    }
}
