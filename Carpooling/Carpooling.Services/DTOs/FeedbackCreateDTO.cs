using Carpooling.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Carpooling.Services.DTOs
{
    public class FeedbackCreateDTO
    {
        [Required]
        public double Rating { get; set; }

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
