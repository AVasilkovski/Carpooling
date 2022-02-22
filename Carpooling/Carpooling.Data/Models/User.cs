using Carpooling.Data.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Carpooling.Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, MinLength(2), MaxLength(20)]
        public string Username { get; set; }

        [Required]
        [RegularExpression(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=-]).*$")]
        public string Password { get; set; }

        [Required, MinLength(2), MaxLength(20)]
        public string FirstName { get; set; }

        [Required, MinLength(2), MaxLength(20)]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string ProfilePictureName { get; set; }

        [Required]
        public UserStatus UserStatus { get; set; }

        public double RatingAsDriver { get; set; }

        public double RatingAsPassenger { get; set; }

        public ICollection<Role> Roles { get; set; } = new List<Role>();

        public ICollection<Travel> TravelsAsPassenger { get; set; } = new List<Travel>();

        public ICollection<Travel> TravelsAsDriver { get; set; } = new List<Travel>();

        public ICollection<Travel> AppliedTravels { get; set; } = new List<Travel>();

        public ICollection<Feedback> RecievedFeedbacks { get; set; } = new List<Feedback>();

        public ICollection<Feedback> GivenFeedbacks { get; set; } = new List<Feedback>();
    }
}
