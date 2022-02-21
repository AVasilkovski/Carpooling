using Carpooling.Data.Models;
using System.Linq;

namespace Carpooling.Services.DTOs
{
    static public class ServiceMapper
    {
        public static UserPresentDTO ToUserDTO(this User user)
        {
            var roles = user.Roles.Select(role => role.Name);
            return new UserPresentDTO(user, roles);
        }

        public static User ToUser(this UserCreateDTO userCreateDTO)
        {
            return new User()
            {
                FirstName = userCreateDTO.FirstName,
                LastName = userCreateDTO.LastName,
                Password = userCreateDTO.Password,
                PhoneNumber = userCreateDTO.PhoneNumber,
                Email = userCreateDTO.Email,
                Username = userCreateDTO.Username
            };
        }

        public static TravelPresentDTO ToTravelDTO(this Travel travel)
        {
            var driver = ToUserDTO(travel.Driver);
            var passengers = travel.Passengers.Select(passenger => ToUserDTO(passenger));
            var startAddress = ToAddressDTO(travel.StartPointCity);
            var endAddress = ToAddressDTO(travel.EndPointCity);
            var applayingPassengers = travel.ApplyingPassengers.Select(passenger => ToUserDTO(passenger));
            var tags = travel.TravelTags.Select(tag => tag.Tag);
            var feedbacks = travel.Feedbacks.Select(feedback => ToFeedbackDTO(feedback));
            return new TravelPresentDTO(travel, startAddress, endAddress, driver, passengers, applayingPassengers, feedbacks, tags);
        }

        public static Travel ToTravel(this TravelCreateDTO travelCreateDTO)
        {
            return new Travel()
            {
                DriverId = travelCreateDTO.DriverId,
                DepartureTime = travelCreateDTO.DepartureTime,
                FreeSpots = travelCreateDTO.FreeSpots,
                StartPointCity = ToAddress(travelCreateDTO.StartPointAddress),
                EndPointCity = ToAddress(travelCreateDTO.EndPointAddress)
            };
        }

        public static FeedbackPresentDTO ToFeedbackDTO(this Feedback feedback)
        {
            var userFrom = ToUserDTO(feedback.UserFrom);
            var userTo = ToUserDTO(feedback.UserTo);
            return new FeedbackPresentDTO(feedback, userFrom, userTo);
        }

        public static Feedback ToFeedback(this FeedbackCreateDTO feedbackCreateDTO)
        {
            return new Feedback()
            {
                Rating = feedbackCreateDTO.Rating,
                Comment = feedbackCreateDTO.Comment,
                UserFromId = feedbackCreateDTO.UserFromId,
                UserToId = feedbackCreateDTO.UserToId,
                TravelId = feedbackCreateDTO.TravelId,
                Type = feedbackCreateDTO.Type
            };
        }

        public static CityPresentDTO ToAddressDTO(City city)
        {
            return new CityPresentDTO(city);
        }

        public static City ToAddress(CityCreateDTO city)
        {
            return new City { Name = city.City };
        }

        public static TravelTagPresentDTO ToTravelTagPresentDTO(TravelTag travelTag)
        {
            return new TravelTagPresentDTO(travelTag);
        }
    }
}
