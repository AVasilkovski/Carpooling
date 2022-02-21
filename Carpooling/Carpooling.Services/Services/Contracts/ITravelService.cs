using Carpooling.Services.DTOs;
using System.Collections.Generic;

namespace Carpooling.Services.Services.Contracts
{
    public interface ITravelService
    {
        IEnumerable<TravelPresentDTO> GetAll();

        TravelPresentDTO Create(TravelCreateDTO newTravel);

        TravelPresentDTO Get(int id);

        TravelPresentDTO Update(int id, TravelCreateDTO travel);

        void Delete(int id);

        void ApplyAsPassenger(int userId, int travelId);

        void AddPassenger(int userId, int driverId, int travelId);

        void RejectPassenger(int userId, int driverId, int travelId);

        void MarkAsComplete(int travelId);

        void CancelTrip(int travelId);

        void CancelParticipation(int passengerId, int travelId);

        IEnumerable<TravelPresentDTO> SearchAllTravels(string startCity, string destinationCity, string driverName, int? freeSpots, bool sortByDate, bool sortByFreeSpots);

        IEnumerable<TravelPresentDTO> SearchAvailableTravels(string user, string startCity, string destinationCity, string driverName, int? freeSpots, bool sortByDate, bool sortByFreeSpots);

        IEnumerable<TravelPresentDTO> SearchFinishedUserTravels(string user, string startCity, string destinationCity, string driverName, int? freeSpots, bool sortByDate, bool sortByFreeSpots);

        IEnumerable<TravelPresentDTO> SearchAppliedUserTravels(string user, string startCity, string destinationCity, string driverName, int? freeSpots, bool sortByDate, bool sortByFreeSpots);

        IEnumerable<TravelPresentDTO> SearchApprovedUserTravels(string user, string startCity, string destinationCity, string driverName, int? freeSpots, bool sortByDate, bool sortByFreeSpots);

        IEnumerable<TravelPresentDTO> SearchUserAsDriverTravels(string user, string startCity, string destinationCity, int? freeSpots, bool sortByDate, bool sortByFreeSpots);
    }
}
