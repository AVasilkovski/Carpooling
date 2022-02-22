using Carpooling.Data.Models;
using Carpooling.Data.Models.Enums;
using System.Collections.Generic;

namespace Carpooling.Services.DTOs
{
    public class UserPresentDTO
    {
        public UserPresentDTO()
        {

        }

        public UserPresentDTO(User user, IEnumerable<string> roles)
        {
            this.Id = user.Id;
            this.Username = user.Username;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Email = user.Email;
            this.PhoneNumber = user.PhoneNumber;
            this.ProfilePic = user.ProfilePictureName;
            this.Status = user.UserStatus;
            this.Roles = roles;
            this.RatingAsPassenger = user.RatingAsPassenger;
            this.RatingAsDriver = user.RatingAsDriver;
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string ProfilePic { get; set; }

        public double RatingAsDriver { get; set; }

        public double RatingAsPassenger { get; set; }

        public UserStatus Status { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
