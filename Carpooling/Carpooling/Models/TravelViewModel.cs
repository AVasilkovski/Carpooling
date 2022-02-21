using System;

namespace Carpooling.Web.Models
{
    public class TravelViewModel
    {
        public int Id { get; set; }

        public string DriverUsername { get; set; }

        public string StartCity { get; set; }

        public string DestinationCity { get; set; }

        public int FreeSpots { get; set; }

        public DateTime DepartureTime { get; set; }
    }
}
