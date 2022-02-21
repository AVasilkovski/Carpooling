using System;
using System.ComponentModel.DataAnnotations;

namespace Carpooling.Web.Models
{
    public class TravelUpdateViewModel
    {
        [Range(1, 25, ErrorMessage = "The amount of passengers must be positive and at least 1.")]
        public int Spots { get; set; }

        public DateTime DepartureTime { get; set; }
    }
}
