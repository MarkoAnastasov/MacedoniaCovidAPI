using System;
using System.Collections.Generic;

namespace MacedoniaCovidAPIV2.Models
{
    public partial class Cities
    {
        public int Id { get; set; }
        public string City { get; set; }
        public int? Cases { get; set; }
        public int? TodayCases { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
}
