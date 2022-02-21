using Carpooling.Data.Models;
using Carpooling.Data.Models.Enums;

namespace Carpooling.Services.DTOs
{
    public class FeedbackPresentDTO
    {
        public FeedbackPresentDTO()
        {

        }

        public FeedbackPresentDTO(Feedback feedback, UserPresentDTO userFrom, UserPresentDTO userTo)
        {
            this.Rating = feedback.Rating;
            this.Comment = feedback.Comment;
            this.Type = feedback.Type;
            this.UserTo = userTo;
            this.UserFrom = userFrom;
            this.TravelId = feedback.TravelId;

        }

        public double Rating { get; set; }

        public string Comment { get; set; }

        public FeedbackType Type { get; set; }

        public UserPresentDTO UserFrom { get; set; }

        public UserPresentDTO UserTo { get; set; }

        public int TravelId { get; set; }
    }
}
