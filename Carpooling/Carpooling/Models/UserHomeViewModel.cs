using System.Collections.Generic;
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
        public string ProfilePictureName { get; set; }

        public IEnumerable<string> RolesName { get; set; }


    }
}
