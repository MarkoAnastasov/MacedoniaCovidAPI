using MacedoniaCovidAPIV2.Errors;
using MacedoniaCovidAPIV2.Interfaces;
using MacedoniaCovidAPIV2.Models;
using MacedoniaCovidAPIV2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace MacedoniaCovidAPIV2.Services
{
    public class CitiesService : ICitiesService
    {
        private readonly ICitiesRepository _citiesRepository;

        public CitiesService(ICitiesRepository citiesRepository)
        {
            _citiesRepository = citiesRepository;
        }

        public List<Cities> GetAllCities()
        {
            return _citiesRepository.GetAll();
        }

        public Cities GetCityByName(string city, Cities getCity, ValidationError err)
        {
            var cities = _citiesRepository.GetAll();
            getCity = cities.FirstOrDefault(x => x.City.ToLower() == city.ToLower().Trim());

            if(getCity == null)
            {
                err.Value = "Градот не е пронајден!";
            }

            return getCity;
        }

        public Cities AddCity(Cities city, ValidationError err)
        {
            var cities = _citiesRepository.GetAll();
            var validation = true;

            var existingCity = cities.FirstOrDefault(x => x.City.ToLower() == city.City.ToLower().Trim());
            if (existingCity != null)
            {
                err.Value = "Градот постои!";
                validation = false;
            }
            validation = ValidateCity(city, err, validation);

            if(validation == true)
            {
                _citiesRepository.Add(city);
                _citiesRepository.SaveEntities();
            }

            return city;
        }

        public Cities UpdateCity(Cities city, ValidationError err)
        {
            var cities = _citiesRepository.GetAll();
            var validation = true;

            var getCity = cities.FirstOrDefault(x => x.City.ToLower() == city.City.ToLower().Trim());
            if(getCity == null)
            {
                err.Value = "Градот не е пронајден!";
            }
            validation = ValidateCity(city, err, validation);

            if(validation == true)
            {
                getCity.Cases = city.Cases;
                getCity.TodayCases = city.TodayCases;
                getCity.Latitude = city.Latitude;
                getCity.Longitude = city.Longitude;
                _citiesRepository.Update(getCity);
                _citiesRepository.SaveEntities();
            }

            return getCity;
        }

        public void DeleteCity(string city, ValidationError err)
        {
            var cities = _citiesRepository.GetAll();

            var getCity = cities.FirstOrDefault(x => x.City.ToLower() == city.ToLower().Trim());

            if(getCity == null)
            {
                err.Value = "Градот не е пронајден!";
            }
            else
            {
                _citiesRepository.Remove(getCity);
                _citiesRepository.SaveEntities();
            }
        }

        private static bool ValidateCity(Cities city, ValidationError err, bool validation)
        {
            if (city.Cases < 0 || city.TodayCases < 0 || city.Latitude <= 0 || city.Longitude <= 0)
            {
                err.Value = "Внесовте невалиден формат!";
                validation = false;
            }
            else if (city.Latitude.ToString().IndexOf(".") > 2 || city.Longitude.ToString().IndexOf(".") > 2)
            {
                err.Value = "Внесовте невалиден формат!";
                validation = false;
            }
            else if (city.Latitude.ToString().Length < 9 || city.Longitude.ToString().Length < 9)
            {
                err.Value = "Внесовте невалиден формат!";
                validation = false;
            }
            else if (city.City == null || city.Cases == null || city.TodayCases == null || city.Latitude == null || city.Longitude == null)
            {
                err.Value = "Ве молиме пополнете ги сите полиња!";
                validation = false;
            }

            return validation;
        }
    }
}
