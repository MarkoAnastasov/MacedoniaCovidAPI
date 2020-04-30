using MacedoniaCovidAPIV2.Errors;
using MacedoniaCovidAPIV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MacedoniaCovidAPIV2.Interfaces
{
    public interface ICitiesService
    {
        List<Cities> GetAllCities();

        Cities GetCityByName(string city, Cities getCity, ValidationError err);

        Cities AddCity(Cities city, ValidationError err);

        Cities UpdateCity(Cities city, ValidationError err);

        void DeleteCity(string city, ValidationError err);
    }
}
