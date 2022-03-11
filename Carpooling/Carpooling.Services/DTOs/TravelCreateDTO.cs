using System;
using System.Collections.Generic;

namespace Carpooling.Services.DTOs
{
    public class TravelCreateDTO
    {
        public int DriverId { get; set; }

        public string StartPointCityName { get; set; }

        public string EndPointCityName { get; set; }

        public DateTime DepartureTime { get; set; }

        public IEnumerable<string> TravelTags { get; set; }

        public int FreeSpots { get; set; }
    }
}
