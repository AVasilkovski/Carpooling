using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Carpooling.Web.Models
{
    public class TravelCreateViewModel
    {
        [Required(ErrorMessage = "Please enter a city from which you travel will start.")]
        public string StartCity { get; set; }

        [Required(ErrorMessage = "Please enter a city in which your travel will end.")]
        public string DestinationCity { get; set; }

        [Required(ErrorMessage = "Please enter the amount of passengers you want.")]
        [Range(1, 25, ErrorMessage = "The amount of passengers must be positive and at least 1.")]
        public int Spots { get; set; }

        [Required(ErrorMessage = "Please enter the time for departure.")]
        public DateTime DepartureTime { get; set; }

        public IEnumerable<string> SlectedTags { get; set; }

        public SelectList Tags { get; set; }
    }
}
