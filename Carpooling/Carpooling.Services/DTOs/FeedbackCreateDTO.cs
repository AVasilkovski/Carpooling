using Carpooling.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Carpooling.Services.DTOs
{
    public class FeedbackCreateDTO
    {
        [Required(ErrorMessage = "Please pick a rating")]
        [Range(0.1, 5, ErrorMessage = "Rating must be between 0,1 and 5.")]
        public double Rating { get; set; }

        [StringLength(250, MinimumLength = 1, ErrorMessage = "Please write a shorter comment.")]
        public string Comment { get; set; }

        public FeedbackType Type { get; set; }

        [Required]
        public int TravelId { get; set; }

        [Required]
        public int UserFromId { get; set; }

        [Required]
        public int UserToId { get; set; }
    }
}
