using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Carpooling.Data.Models
{
    public class Travel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int StartPointCityId { get; set; }

        public City StartPointCity { get; set; }

        [Required]
        public int EndPointCityId { get; set; }

        public City EndPointCity { get; set; }

        [Required]
        public int DriverId { get; set; }

        public User Driver { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public int FreeSpots { get; set; }

        public bool IsCompleted { get; set; }

        public ICollection<User> Passengers { get; set; } = new List<User>();

        public ICollection<User> ApplyingPassengers { get; set; } = new List<User>();

        public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

        public ICollection<TravelTag> TravelTags { get; set; } = new List<TravelTag>();
    }
}
