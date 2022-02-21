using System;

namespace Carpooling.Web.Models.APIModel
{
    public class TravelRequestModel
    {
        public int DriverId { get; set; }

        public CityResponseModel StartPointAddress { get; set; }

        public CityResponseModel EndPointAddress { get; set; }

        public DateTime DepartureTime { get; set; }

        public int FreeSpots { get; set; }
    }
}
