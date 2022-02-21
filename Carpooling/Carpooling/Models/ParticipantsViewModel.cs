using System.Collections.Generic;

namespace Carpooling.Web.Models
{
    public class ParticipantsViewModel
    {
        public UserProfileViewModel Driver { get; set; }

        public IEnumerable<UserProfileViewModel> Passengers { get; set; }

        public IEnumerable<FeedbackViewModel> Feedbacks { get; set; }
    }
}
