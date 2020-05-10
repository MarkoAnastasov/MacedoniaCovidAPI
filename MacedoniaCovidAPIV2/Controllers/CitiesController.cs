using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MacedoniaCovidAPIV2.Models;
using MacedoniaCovidAPIV2.Interfaces;
using MacedoniaCovidAPIV2.Common.Exceptions;

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
            try
            {
                var cities = _citiesService.GetAllCities();

                return Ok(cities);
            }
            catch(FlowException ex)
            {
                return NotFound(ex.Message);
            }
            catch(Exception)
            {
                return StatusCode(500, "Проблем со апликацијата.Пробајте повторно!");
            }
        }

        [HttpGet("{city}")]
        public ActionResult<Cities> GetCityByName(string city)
        {
            try
            {
                var getCity = _citiesService.GetCityByName(city);

                return Ok(getCity);
            }
            catch (FlowException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Проблем со апликацијата.Пробајте повторно!");
            }
        }

        [HttpPost]
        public ActionResult<Cities> AddCity(Cities city)
        {
            try
            {
                var createdCity = _citiesService.AddCity(city);

                return CreatedAtAction("AddCity", createdCity);
            }
            catch (FlowException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Проблем со апликацијата.Пробајте повторно!");
            }
        }

        [HttpPut]
        public ActionResult<Cities> UpdateCity(Cities city)
        {
            try
            {
                var updatedCity = _citiesService.UpdateCity(city);

                return Ok(updatedCity);
            }
            catch (FlowException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Проблем со апликацијата.Пробајте повторно!");
            }
        }

        [HttpDelete("{city}")]
        public ActionResult DeleteCity(string city)
        {
            try
            {
                _citiesService.DeleteCity(city);
            }
            catch (FlowException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Проблем со апликацијата.Пробајте повторно!");
            }

            return Ok();
        }
    }
}
