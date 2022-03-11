using Carpooling.Data.Models.Enums;
using Carpooling.Services.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Carpooling.Web.Models.APIModel
{
    public class FeedbackResponseModel
    {
        [Required(ErrorMessage = "Please pick a rating")]
        [Range(0.1, 5, ErrorMessage = "Rating must be between 0,1 and 5.")]
        public double Rating { get; set; }

        [StringLength(250, MinimumLength = 1, ErrorMessage = "Please write a shorter comment.")]
        public string Comment { get; set; }

        public FeedbackType Type { get; set; }

        public UserResponseModel UserFromUsername { get; set; }

        public UserResponseModel UserToUsername { get; set; }

        public int TravelId { get; set; }
    }
}
