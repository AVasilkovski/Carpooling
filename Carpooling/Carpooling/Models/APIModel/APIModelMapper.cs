using Carpooling.Data.Models.Enums;
using Carpooling.Services.DTOs;
using System;
using System.Linq;

namespace Carpooling.Web.Models.APIModel
{
    public static class APIModelMapper
    {
        public static TravelCreateDTO TravelRequestModel(this TravelRequestModel travelRequest)
        {
            var result = new TravelCreateDTO
            {
                DriverId = travelRequest.DriverId,
                StartPointAddress = AddressDTOModel(travelRequest.StartPointAddress),
                EndPointAddress = AddressDTOModel(travelRequest.EndPointAddress),
                DepartureTime = travelRequest.DepartureTime,
                FreeSpots = travelRequest.FreeSpots
            };

            return result;
        }

        public static TravelResponseModel TravelResponseModel(this TravelPresentDTO travelToBeDisplayed)
        {
            var startAddress = AddressResponseModel(travelToBeDisplayed.StartPointAddress);
            var endAddress = AddressResponseModel(travelToBeDisplayed.EndPointAddress);
            var passengers = travelToBeDisplayed.Passengers.Select(passenger => UserResponseModel(passenger));
            var result = new TravelResponseModel(travelToBeDisplayed, startAddress, endAddress, passengers);

            return result;
        }

        public static CityResponseModel ToAdressResponse(CityCreateDTO cityCreateDTO)
        {
            var result = new CityResponseModel(cityCreateDTO.City);
            return result;
        }

        public static CityResponseModel AddressResponseModel(CityPresentDTO addressPresentDTO)
        {
            var result = new CityResponseModel(addressPresentDTO.City);

            return result;
        }

        public static CityCreateDTO AddressDTOModel(CityResponseModel addressResponseModel)
        {

            var result = new CityCreateDTO
            {
                City = addressResponseModel.City
            };

            return result;
        }


        public static UserResponseModel UserResponseModel(this UserPresentDTO userPresentDTO)
        {
            var result = new UserResponseModel(userPresentDTO);

            return result;
        }

        public static UserCreateDTO UserRequestModel(this UserRequestModel userRequestModel)
        {
            var result = new UserCreateDTO
            {
                FirstName = userRequestModel.FirstName,
                LastName = userRequestModel.LastName,
                Username = userRequestModel.Username,
                Password = userRequestModel.Password,
                Email = userRequestModel.Email,
                PhoneNumber = userRequestModel.PhoneNumber,
                ProfilePicture = userRequestModel.ProfilePicture
            };
            return result;
        }
        public static FeedbackResponseModel FeedbackResponseModel(this FeedbackPresentDTO feedbackPresentDTO)
        {
            var userFrom = UserResponseModel(feedbackPresentDTO.UserFrom);
            var userTo = UserResponseModel(feedbackPresentDTO.UserFrom);
            var result = new FeedbackResponseModel(feedbackPresentDTO, userFrom, userTo);
            return result;
        }

        public static FeedbackCreateDTO FeedbackRequestModel(this FeedbackRequestModel feedbackRequestModel)
        {
            var result = new FeedbackCreateDTO
            {
                Comment = feedbackRequestModel.Comment,
                TravelId = feedbackRequestModel.TravelId,
                UserFromId = feedbackRequestModel.UserFromId,
                UserToId = feedbackRequestModel.UserToId,
                Rating = feedbackRequestModel.Rating,
                Type = Enum.Parse<FeedbackType>(feedbackRequestModel.Type, true)
            };

            return result;
        }

    }
}
