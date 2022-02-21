using Carpooling.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Carpooling.Data.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }

        [Required, MinLength(0), MaxLength(5)]
        public double Rating { get; set; }

        public string Comment { get; set; }

        [Required]
        public FeedbackType Type { get; set; }

        [Required]
        public int TravelId { get; set; }

        public Travel Travel { get; set; }

        [Required]
        public int UserFromId { get; set; }

        public User UserFrom { get; set; }

        [Required]
        public int UserToId { get; set; }

        public User UserTo { get; set; }
    }
}
