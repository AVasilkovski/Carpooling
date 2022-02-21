using System;
using System.Collections.Generic;

namespace Carpooling.Web.Models
{
    public class TravelDetailsViewModel
    {
        public int Id { get; set; }

        public string StartCity { get; set; }

        public string DestinationCity { get; set; }

        public DateTime DepartureTime { get; set; }

        public bool Completed { get; set; }

        public int Spots { get; set; }

        public int AvailableSpots { get; set; }

        public UserProfileViewModel Driver { get; set; }

        public IEnumerable<UserProfileViewModel> Participants { get; set; }

        public IEnumerable<UserProfileViewModel> Applicants { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}
