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

        Cities GetCityByName(string city);

        Cities AddCity(Cities city);

        Cities UpdateCity(Cities city);

        void DeleteCity(string city);
    }
}
