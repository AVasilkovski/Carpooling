using System.ComponentModel.DataAnnotations;

namespace Carpooling.Services.DTOs
{
    public class UserCreateDTO
    {
        [Required(ErrorMessage = "Please enter a first name")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 20 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a last name")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 20 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter a username")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Username must be between 2 and 20 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [RegularExpression(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=-]).*$",
         ErrorMessage = "Password must be at least 8 characters long and must contain at least one capital letter, one digit and one special symbol like ( !, *, @, #, $, %, ^, &, +, =, - )")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter an email address")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a phone number")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number must be 10 digits")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }

        public string ProfilePictureName { get; set; }
    }
}
