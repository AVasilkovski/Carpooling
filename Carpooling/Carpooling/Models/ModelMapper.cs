using Carpooling.Services.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace Carpooling.Web.Models
{
    public static class ModelMapper
    {
        public static UserCreateDTO ToUserCreateDTO(this RegisterViewModel registerViewModel)
        {
            return new UserCreateDTO()
            {
                Username = registerViewModel.Username,
                Password = registerViewModel.Password,
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                Email = registerViewModel.Email,
                PhoneNumber = registerViewModel.PhoneNumber
            };
        }

        public static UserHomeViewModel ToUserHomeViewModel(this UserPresentDTO userPresentDTO)
        {
            return new UserHomeViewModel()
            {
                FirstName = userPresentDTO.FirstName,
                LastName = userPresentDTO.LastName,
                ProfilePicture = userPresentDTO.ProfilePic,
            };
        }

        public static UserCreateDTO ToUserUpdate(this UserUpdateViewModel userUpdateViewModel, string profilePicture)
        {
            return new UserCreateDTO()
            {
                Email = userUpdateViewModel.Email,
                FirstName = userUpdateViewModel.FirstName,
                LastName = userUpdateViewModel.LastName,
                Password = userUpdateViewModel.Password,
                PhoneNumber = userUpdateViewModel.PhoneNumber,
                ProfilePicture = profilePicture
            };
        }

        public static UserUpdateViewModel ToUserUpdateViewModel(this UserPresentDTO userPresentDTO)
        {
            return new UserUpdateViewModel()
            {
                Email = userPresentDTO.Email,
                FirstName = userPresentDTO.FirstName,
                LastName = userPresentDTO.LastName,
                PhoneNumber = userPresentDTO.PhoneNumber,
            };
        }

        public static UserProfileViewModel ToUserProfileViewModel(this UserPresentDTO userPresentDTO)
        {
            return new UserProfileViewModel()
            {
                Id = userPresentDTO.Id,
                ProfilePicture = userPresentDTO.ProfilePic,
                Username = userPresentDTO.Username,
                Email = userPresentDTO.Email,
                FirstName = userPresentDTO.FirstName,
                LastName = userPresentDTO.LastName,
                PhoneNumber = userPresentDTO.PhoneNumber,
                RatingAsDriver = userPresentDTO.RatingAsDriver,
                RatingAsPassenger = userPresentDTO.RatingAsPassenger
            };
        }

        public static UserViewModel ToUserViewModel(this UserPresentDTO userPresentDTO)
        {
            string status = userPresentDTO.Status.ToString();
            return new UserViewModel()
            {
                Id = userPresentDTO.Id,
                Username = userPresentDTO.Username,
                Email = userPresentDTO.Email,
                PhoneNumber = userPresentDTO.PhoneNumber,
                Status = status
            };
        }

        public static TravelViewModel ToTravelViewModel(this TravelPresentDTO travelPresentDTO)
        {
            return new TravelViewModel()
            {
                Id = travelPresentDTO.Id,
                DriverUsername = travelPresentDTO.Driver.Username,
                StartCity = travelPresentDTO.StartPointAddress.City,
                DestinationCity = travelPresentDTO.EndPointAddress.City,
                FreeSpots = travelPresentDTO.FreeSpots,
                DepartureTime = travelPresentDTO.DepartureTime
            };
        }

        public static TravelCreateDTO ToTravelCreateDTO(this TravelCreateViewModel travelCreateViewModel, int driverId)
        {
            var startCity = new CityCreateDTO() { City = travelCreateViewModel.StartCity };
            var destinationCity = new CityCreateDTO() { City = travelCreateViewModel.DestinationCity };
            return new TravelCreateDTO()
            {
                DriverId = driverId,
                StartPointAddress = startCity,
                EndPointAddress = destinationCity,
                FreeSpots = travelCreateViewModel.Spots,
                DepartureTime = travelCreateViewModel.DepartureTime,
                TravelTags = travelCreateViewModel.SlectedTags
            };
        }

        public static TravelCreateDTO ToTravelUpdate(this TravelUpdateViewModel travelUpdateViewModel)
        {
            return new TravelCreateDTO()
            {
                DepartureTime = travelUpdateViewModel.DepartureTime,
                FreeSpots = travelUpdateViewModel.Spots
            };
        }

        public static TravelDetailsViewModel ToTravelDetailsViewModel(this TravelPresentDTO travelPresentDTO)
        {
            var applicants = travelPresentDTO.ApplyingPassengers.Select(passenger => passenger.ToUserProfileViewModel());
            var passengers = travelPresentDTO.Passengers.Select(passenger => passenger.ToUserProfileViewModel());
            var availableSpots = travelPresentDTO.FreeSpots - passengers.Count();
            return new TravelDetailsViewModel()
            {
                Id = travelPresentDTO.Id,
                StartCity = travelPresentDTO.StartPointAddress.City,
                DestinationCity = travelPresentDTO.EndPointAddress.City,
                Completed = travelPresentDTO.IsCompleted,
                Spots = travelPresentDTO.FreeSpots,
                AvailableSpots = availableSpots,
                DepartureTime = travelPresentDTO.DepartureTime,
                Driver = travelPresentDTO.Driver.ToUserProfileViewModel(),
                Applicants = applicants,
                Participants = passengers,
                Tags = travelPresentDTO.TravelTags
            };
        }

        public static FeedbackSearchViewModel ToFeedbackReceivedViewModel(this FeedbackPresentDTO feedbackPresentDTO)
        {
            return new FeedbackSearchViewModel()
            {
                UserId = feedbackPresentDTO.UserFrom.Id,
                Username = feedbackPresentDTO.UserFrom.Username,
                FullName = feedbackPresentDTO.UserFrom.FirstName + " " + feedbackPresentDTO.UserFrom.LastName,
                Comment = feedbackPresentDTO.Comment,
                Rating = feedbackPresentDTO.Rating
            };
        }

        public static FeedbackSearchViewModel ToFeedbackGivenViewModel(this FeedbackPresentDTO feedbackPresentDTO)
        {
            return new FeedbackSearchViewModel()
            {
                UserId = feedbackPresentDTO.UserTo.Id,
                Username = feedbackPresentDTO.UserTo.Username,
                FullName = feedbackPresentDTO.UserTo.FirstName + feedbackPresentDTO.UserTo.LastName,
                Comment = feedbackPresentDTO.Comment,
                Rating = feedbackPresentDTO.Rating
            };
        }

        public static FeedbackViewModel ToFeedbackViewModel(this FeedbackPresentDTO feedbackPresentDTO)
        {
            return new FeedbackViewModel()
            {
                Username = feedbackPresentDTO.UserFrom.Username
            };
        }

        public static FeedbackCreateDTO ToFeedbackCreateDTO(this FeedbackCreateViewModel feedbackCreateViewModel)
        {
            return new FeedbackCreateDTO()
            {
                Comment = feedbackCreateViewModel.Comment,
                Rating = feedbackCreateViewModel.Rating
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
