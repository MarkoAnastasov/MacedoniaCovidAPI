using MacedoniaCovidAPIV2.Interfaces;
using MacedoniaCovidAPIV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MacedoniaCovidAPIV2.Repositories
{
    public class CitiesRepository : BaseRepository<Cities>, ICitiesRepository
    {
        public CitiesRepository(MkdCitiesCovid19Context context) : base(context)
        {

        }
    }
}
