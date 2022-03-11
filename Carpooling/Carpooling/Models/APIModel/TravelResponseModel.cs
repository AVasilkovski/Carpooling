using Carpooling.Services.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Carpooling.Web.Models.APIModel
{
    public class TravelResponseModel
    {
        public int TravelId { get; }

        [Required(ErrorMessage = "Please enter a city from which you travel will start.")]
        public string StartPointCityName { get; set; }

        [Required(ErrorMessage = "Please enter a city in which your travel will end.")]
        public string EndPointCityName { get; set; }

        public string DriverFirstName { get; set; }

        public string DriverLastName { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        [Range(1, 25, ErrorMessage = "The amount of passengers must be positive and at least 1.")]
        public int FreeSpots { get; set; }

        public bool IsCompleted { get; set; }

        public IEnumerable<UserResponseModel> Passengers { get; set; }

        public IEnumerable<string> TravelTags { get; set; }

    }
}
