using Carpooling.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Carpooling.Services.Services.Contracts
{
    public interface ITravelService
    {
        IEnumerable<TravelPresentDTO> GetAll();

        Task<TravelPresentDTO> CreateAsync(TravelCreateDTO newTravel);

        Task<TravelPresentDTO> GetAsync(int id);

        Task<TravelPresentDTO> UpdateAsync(int id, TravelCreateDTO travel);

        Task DeleteAsync(int id);

        Task ApplyAsPassengerAsync(int userId, int travelId);

        Task AddPassengerAsync(int userId, int driverId, int travelId);

        Task RejectPassengerAsync(int userId, int driverId, int travelId);

        Task MarkAsCompleteAsync(int travelId);

        Task CancelTripAsync(int travelId);

        Task CancelParticipationAsync(int passengerId, int travelId);

        IEnumerable<TravelPresentDTO> SearchAllTravels(string startCity, string destinationCity, string driverName, int? freeSpots, bool sortByDate, bool sortByFreeSpots);

        IEnumerable<TravelPresentDTO> SearchAvailableTravels(string user, string startCity, string destinationCity, string driverName, int? freeSpots, bool sortByDate, bool sortByFreeSpots);

        IEnumerable<TravelPresentDTO> SearchFinishedUserTravels(string user, string startCity, string destinationCity, string driverName, int? freeSpots, bool sortByDate, bool sortByFreeSpots);

        IEnumerable<TravelPresentDTO> SearchAppliedUserTravels(string user, string startCity, string destinationCity, string driverName, int? freeSpots, bool sortByDate, bool sortByFreeSpots);

        IEnumerable<TravelPresentDTO> SearchApprovedUserTravels(string user, string startCity, string destinationCity, string driverName, int? freeSpots, bool sortByDate, bool sortByFreeSpots);

        IEnumerable<TravelPresentDTO> SearchUserAsDriverTravels(string user, string startCity, string destinationCity, int? freeSpots, bool sortByDate, bool sortByFreeSpots);
    }
}
