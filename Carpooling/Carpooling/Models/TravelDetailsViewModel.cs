using System;
using System.Collections.Generic;

namespace Carpooling.Web.Models
{
    public class TravelDetailsViewModel
    {
        public int Id { get; set; }

        public string StartPointCityName { get; set; }

        public string EndPointCityName { get; set; }

        public DateTime DepartureTime { get; set; }

        public bool Completed { get; set; }

        public int FreeSpots { get; set; }

        public int AvailableSpots { get; set; }

        public UserProfileViewModel Driver { get; set; }

        public IEnumerable<UserProfileViewModel> Passengers { get; set; }

        public IEnumerable<UserProfileViewModel> ApplyingPassengers { get; set; }

        public IEnumerable<string> TravelTags { get; set; }
    }
}
