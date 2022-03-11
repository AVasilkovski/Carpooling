using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Carpooling.Web.Models
{
    public class UserUpdateViewModel
    {
        [Display(Name = "Profile picture")]
        public IFormFile IProfilePictureName { get; set; }

        public string ProfilePictureName { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Email address")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Display(Name = "First name")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 20 characters")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 20 characters")]
        public string LastName { get; set; }

        [Display(Name = "Phone number")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number must be 10 digits")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }

        public IEnumerable<string> RolesName { get; set; }
    }
}
