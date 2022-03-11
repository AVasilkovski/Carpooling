using Carpooling.Data.Models;
using Carpooling.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Carpooling.Services.DTOs
{
    public class FeedbackPresentDTO
    {
        [Required]
        public double Rating { get; set; }

        public string Comment { get; set; }

        public FeedbackType Type { get; set; }

        [Required]
        public string UserFromUsername { get; set; }

        [Required]
        public string UserToUsername { get; set; }

        //public UserPresentDTO UserTo { get; set; }

        [Required]
        public int TravelId { get; set; }
    }
}
