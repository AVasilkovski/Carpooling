using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Carpooling.Web.Models
{
    public class UserProfileViewModel : UserHomeViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Username")]
        public string Username { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Rating as driver")]
        public double RatingAsDriver { get; set; }

        [Display(Name = "Rating as passenger")]
        public double RatingAsPassenger { get; set; }

    }
}
