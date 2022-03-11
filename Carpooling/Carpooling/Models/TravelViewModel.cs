using System;

namespace Carpooling.Web.Models
{
    public class TravelViewModel
    {
        public int Id { get; set; }

        public string DriverUsername { get; set; }

        public string StartPointCityName { get; set; }

        public string EndPointCityName { get; set; }

        public int FreeSpots { get; set; }

        public DateTime DepartureTime { get; set; }
    }
}
