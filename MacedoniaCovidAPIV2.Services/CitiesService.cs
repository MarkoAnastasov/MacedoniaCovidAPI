using MacedoniaCovidAPIV2.Common.Exceptions;
using MacedoniaCovidAPIV2.Interfaces;
using MacedoniaCovidAPIV2.Models;
using MacedoniaCovidAPIV2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
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

        public Cities GetCityByName(string city)
        {
            var getCity = _citiesRepository.GetFirstWhere(x => x.City.ToLower() == city.ToLower().Trim());

            if(getCity == null)
            {
                throw new FlowException("Градот не е пронајден!");
            }

            return getCity;
        }

        public Cities AddCity(Cities city)
        {
            var existingCity = _citiesRepository.GetFirstWhere(x => x.City.ToLower() == city.City.ToLower().Trim());

            if (existingCity != null)
            {
                throw new FlowException("Градот постои!");
            }
            
            ValidateCity(city);

            _citiesRepository.Add(city);
            _citiesRepository.SaveEntities();

            return city;
        }

        public Cities UpdateCity(Cities city)
        {

            var getCity = _citiesRepository.GetFirstWhere(x => x.City.ToLower() == city.City.ToLower().Trim());

            if(getCity == null)
            {
                throw new FlowException("Градот не е пронајден!");
            }

            ValidateCity(city);

            getCity.Cases = city.Cases;
            getCity.TodayCases = city.TodayCases;
            getCity.Latitude = city.Latitude;
            getCity.Longitude = city.Longitude;

            _citiesRepository.Update(getCity);
            _citiesRepository.SaveEntities();

            return getCity;
        }

        public void DeleteCity(string city)
        {
            var getCity = _citiesRepository.GetFirstWhere(x => x.City.ToLower() == city.ToLower().Trim());

            if(getCity == null)
            {
                throw new FlowException("Градот не е пронајден!");
            }

            _citiesRepository.Remove(getCity);
            _citiesRepository.SaveEntities();
        }

        private static void ValidateCity(Cities city)
        {
            if (city.Cases < 0 || city.TodayCases < 0 || city.Latitude <= 0 || city.Longitude <= 0)
            {
                throw new FlowException("Внесовте невалиден формат!");
            }
            else if (city.Latitude.ToString().IndexOf(".") > 2 || city.Longitude.ToString().IndexOf(".") > 2)
            {
                throw new FlowException("Внесовте невалиден формат!");
            }
            else if (city.Latitude.ToString().Length < 9 || city.Longitude.ToString().Length < 9)
            {
                throw new FlowException("Внесовте невалиден формат!");
            }
            else if (city.City == null || city.Cases == null || city.TodayCases == null || city.Latitude == null || city.Longitude == null)
            {
                throw new FlowException("Ве молиме пополнете ги сите полиња!");
            }
        }
    }
}
