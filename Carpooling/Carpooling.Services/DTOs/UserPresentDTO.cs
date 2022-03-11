using Carpooling.Data.Models;
using Carpooling.Data.Models.Enums;
using System.Collections.Generic;

namespace Carpooling.Services.DTOs
{
    public class UserPresentDTO
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string ProfilePictureName { get; set; }

        public double RatingAsDriver { get; set; }

        public double RatingAsPassenger { get; set; }

        public UserStatus UserStatus { get; set; }

        public string Role { get; set; }
    }
}
