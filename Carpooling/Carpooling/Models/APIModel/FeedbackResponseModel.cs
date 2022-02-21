using Carpooling.Services.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Carpooling.Web.Models.APIModel
{
    public class FeedbackResponseModel
    {
        public FeedbackResponseModel(FeedbackPresentDTO feedbackPresentDTO, UserResponseModel userFrom, UserResponseModel userTo)
        {
            this.Rating = feedbackPresentDTO.Rating;
            this.Type = feedbackPresentDTO.Type.ToString();
            this.UserFrom = userFrom;
            this.UserTo = userTo;
            this.TravelId = feedbackPresentDTO.TravelId;
            this.Comment = feedbackPresentDTO.Comment;
        }

        [Required(ErrorMessage = "Please pick a rating")]
        [Range(0.1, 5, ErrorMessage = "Rating must be between 0,1 and 5.")]
        public double Rating { get; set; }

        [StringLength(250, MinimumLength = 1, ErrorMessage = "Please write a shorter comment.")]
        public string Comment { get; set; }

        public string Type { get; set; }

        public UserResponseModel UserFrom { get; set; }

        public UserResponseModel UserTo { get; set; }

        public int TravelId { get; set; }
    }
}
