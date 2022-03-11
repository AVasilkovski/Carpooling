using Carpooling.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Carpooling.Services.DTOs
{
    public class TravelPresentDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a city from which you travel will start.")]
        public string StartPointCityName { get; set; }

        [Required(ErrorMessage = "Please enter a city in which your travel will end.")]
        public string EndPointCityName { get; set; }

        public UserPresentDTO Driver { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        [Range(1, 25, ErrorMessage = "The amount of passengers must be positive and at least 1.")]
        public int FreeSpots { get; set; }

        public bool IsCompleted { get; set; }

        public IEnumerable<UserPresentDTO> Passengers { get; set; }

        public IEnumerable<UserPresentDTO> ApplyingPassengers { get; set; }

        public IEnumerable<FeedbackPresentDTO> Feedbacks { get; set; }

        public IEnumerable<string> TravelTags { get; set; }
    }
}
