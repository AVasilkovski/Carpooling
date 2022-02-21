using Carpooling.Services.DTOs;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Carpooling.Web.Models.APIModel
{
    public class UserResponseModel
    {
        public UserResponseModel(UserPresentDTO userPresentDTO)
        {
            this.Id = userPresentDTO.Id;
            this.Username = userPresentDTO.Username;
            this.FirstName = userPresentDTO.FirstName;
            this.LastName = userPresentDTO.LastName;
            this.Email = userPresentDTO.Email;
            this.PhoneNumber = userPresentDTO.PhoneNumber;
            this.ProfilePic = userPresentDTO.ProfilePic;
            this.Status = userPresentDTO.Status.ToString();
            this.Roles = userPresentDTO.Roles;
            this.RatingAsDriver = userPresentDTO.RatingAsDriver;
            this.RatingAsPassenger = userPresentDTO.RatingAsPassenger;
        }
        public int Id { get; set; }

        [StringLength(20, MinimumLength = 2, ErrorMessage = "Username must be between 2 and 20 characters")]
        public string Username { get; set; }

        [StringLength(20, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 20 characters")]
        public string FirstName { get; set; }

        [StringLength(20, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 20 characters")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number must be 10 digits")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }

        public string ProfilePic { get; set; }

        public double RatingAsDriver { get; set; }

        public double RatingAsPassenger { get; set; }

        public string Status { get; set; }

        public IEnumerable<string> Roles { get; set; }

        [RegularExpression(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=-]).*$",
        ErrorMessage = "Password must be at least 8 characters long and must contain at least one capital letter, one digit and one special symbol like ( !, *, @, #, $, %, ^, &, +, =, - )")]
        public string Password { get; set; }
    }
}
