using Carpooling.Data.Models;
using System;
using System.Collections.Generic;


namespace Carpooling.Services.DTOs
{
    public class TravelPresentDTO
    {
        public TravelPresentDTO()
        {

        }

        public TravelPresentDTO(Travel travel, CityPresentDTO startPoint, CityPresentDTO endPoint, UserPresentDTO driver, IEnumerable<UserPresentDTO> passengers,
            IEnumerable<UserPresentDTO> applyingPassengers, IEnumerable<FeedbackPresentDTO> feedbacks, IEnumerable<string> travelTags)
        {
            this.Id = travel.Id;
            this.StartPointAddress = startPoint;
            this.EndPointAddress = endPoint;
            this.Driver = driver;
            this.DepartureTime = travel.DepartureTime;
            this.FreeSpots = travel.FreeSpots;
            this.IsCompleted = travel.IsCompleted;
            this.Passengers = passengers;
            this.ApplyingPassengers = applyingPassengers;
            this.TravelTags = travelTags;
            this.Feedbacks = feedbacks;
        }


        public int Id { get; set; }

        public CityPresentDTO StartPointAddress { get; set; }

        public CityPresentDTO EndPointAddress { get; set; }

        public UserPresentDTO Driver { get; set; }

        public DateTime DepartureTime { get; set; }

        public int FreeSpots { get; set; }

        public bool IsCompleted { get; set; }

        public IEnumerable<UserPresentDTO> Passengers { get; set; }

        public IEnumerable<UserPresentDTO> ApplyingPassengers { get; set; }

        public IEnumerable<FeedbackPresentDTO> Feedbacks { get; set; }

        public IEnumerable<string> TravelTags { get; set; }
    }
}
