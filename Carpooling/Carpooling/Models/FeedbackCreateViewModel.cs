using System;
using System.ComponentModel.DataAnnotations;

namespace Carpooling.Web.Models
{
    public class FeedbackCreateViewModel
    {
        [StringLength(250, MinimumLength = 1, ErrorMessage = "Please write a shorter comment.")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "Please pick a rating")]
        [Range(0.1, 5, ErrorMessage = "Rating must be between 0,1 and 5.")]
        public double Rating { get; set; }
    }
}
