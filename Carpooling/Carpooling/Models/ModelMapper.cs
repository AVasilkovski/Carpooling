using Carpooling.Services.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace Carpooling.Web.Models
{
    public static class ModelMapper
    {
        

        public static UserProfileViewModel ToUserProfileViewModel(this UserPresentDTO userPresentDTO)
        {
            return new UserProfileViewModel()
            {
                Id = userPresentDTO.Id,
                ProfilePictureName = userPresentDTO.ProfilePictureName,
                Username = userPresentDTO.Username,
                Email = userPresentDTO.Email,
                FirstName = userPresentDTO.FirstName,
                LastName = userPresentDTO.LastName,
                PhoneNumber = userPresentDTO.PhoneNumber,
                RatingAsDriver = userPresentDTO.RatingAsDriver,
                RatingAsPassenger = userPresentDTO.RatingAsPassenger
            };
        }

        
        public static ParticipantsViewModel ToParticipantsViewModel(this UserProfileViewModel driver, IEnumerable<UserProfileViewModel> passengers, IEnumerable<FeedbackViewModel> feedbacks)
        {
            return new ParticipantsViewModel()
            {
                Driver = driver,
                Feedbacks = feedbacks,
                Passengers = passengers,
            };
        }
    }
}
