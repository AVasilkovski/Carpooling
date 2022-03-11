using System;

namespace Carpooling.Web.Models.APIModel
{
    public class TravelRequestModel
    {
        public int DriverId { get; set; }

        public string StartPointCityName { get; set; }

        public string EndPointCityName { get; set; }

        public DateTime DepartureTime { get; set; }

        public int FreeSpots { get; set; }
    }
}
