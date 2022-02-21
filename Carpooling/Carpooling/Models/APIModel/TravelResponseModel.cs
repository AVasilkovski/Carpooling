using Carpooling.Services.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Carpooling.Web.Models.APIModel
{
    public class TravelResponseModel
    {
        public TravelResponseModel(TravelPresentDTO travelPresentDTO, CityResponseModel startAddressResponseModel,
                                  CityResponseModel endAddressResponseModel, IEnumerable<UserResponseModel> passengers)
        {
            this.TravelId = travelPresentDTO.Id;
            this.StartPointAddress = startAddressResponseModel;
            this.EndPointAddress = endAddressResponseModel;
            this.DepartureTime = travelPresentDTO.DepartureTime;
            this.FreeSpots = travelPresentDTO.FreeSpots;
            this.IsCompleted = travelPresentDTO.IsCompleted;
            this.DriverFirstName = travelPresentDTO.Driver.FirstName;
            this.DriverLastName = travelPresentDTO.Driver.LastName;
            this.Passengers = passengers;
            this.TravelTags = travelPresentDTO.TravelTags;
        }
        public int TravelId { get; }

        [Required(ErrorMessage = "Please enter a city from which you travel will start.")]
        public CityResponseModel StartPointAddress { get; set; }

        [Required(ErrorMessage = "Please enter a city in which your travel will end.")]
        public CityResponseModel EndPointAddress { get; set; }

        public string DriverFirstName { get; set; }

        public string DriverLastName { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        [Range(1, 25, ErrorMessage = "The amount of passengers must be positive and at least 1.")]
        public int FreeSpots { get; set; }

        public bool IsCompleted { get; set; }

        public IEnumerable<UserResponseModel> Passengers { get; set; }

        public IEnumerable<string> TravelTags { get; set; }

    }
}
