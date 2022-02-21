using System.ComponentModel.DataAnnotations;

namespace Carpooling.Web.Models
{
    public class UserHomeViewModel
    {
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Profile picture")]
        public string ProfilePicture { get; set; }
    }
}
