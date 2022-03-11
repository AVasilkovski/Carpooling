using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Carpooling.Data;
using Carpooling.Data.Models;
using Carpooling.Services.DTOs;
using Carpooling.Services.Services.Contracts;
using Carpooling.Services.Exceptions;
using System.Threading.Tasks;
using AutoMapper;

namespace Carpooling.Services.Services
{
    public class TravelService : ITravelService
    {
        private readonly CarpoolingContext dbContext;
        private readonly ICityService cityService;
        private readonly IMapper mapper;
        private readonly ITravelTagService travelTagService;

        public TravelService(CarpoolingContext dbContext, ITravelTagService travelTagService, ICityService cityService, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.travelTagService = travelTagService;
            this.cityService = cityService;
            this.mapper = mapper;
        }

        private IQueryable<Travel> TravelsQuery
        {
            get
            {
                return this.dbContext.Travels.Include(start => start.StartPointCity)
                                               .Include(end => end.EndPointCity)
                                               .Include(driver => driver.Driver)
                                                       .ThenInclude(role => role.Role)
                                               .Include(passenger => passenger.Passengers)
                                                      .ThenInclude(role => role.Role)
                                               .Include(applying => applying.ApplyingPassengers)
                                                      .ThenInclude(role => role.Role)
                                               .Include(feedback => feedback.Feedbacks)
                                               .Include(tag => tag.TravelTags);
            }
        }

        public IEnumerable<TravelPresentDTO> GetAll()
        {
            return this.TravelsQuery.Select(travel => mapper.Map<TravelPresentDTO>(travel));
        }

        public async Task<TravelPresentDTO> GetAsync(int id)
        {
            var result = await this.GetTravelAsync(id);

            return this.mapper.Map<TravelPresentDTO>(result);
        }

        public async Task<TravelPresentDTO> CreateAsync(TravelCreateDTO newTravel)
        {
            var startCity = await this.cityService.CheckIfCityExistAsync(newTravel.StartPointCityName);
            var destinationCity = await this.cityService.CheckIfCityExistAsync(newTravel.EndPointCityName);
            var travel = this.mapper.Map<TravelPresentDTO>(newTravel);
            travel.StartPointCityName = startCity;
            travel.EndPointCityName = destinationCity;
            travel.TravelTags = (IEnumerable<string>)this.travelTagService.FindTags(newTravel.TravelTags);
            await this.dbContext.Travels.AddAsync(this.mapper.Map<Travel>(travel));
            await this.dbContext.SaveChangesAsync();
            var createdTravel = await this.GetTravelAsync(travel.Id);

            return this.mapper.Map<TravelPresentDTO>(createdTravel);
        }

        public async Task<TravelPresentDTO> UpdateAsync(int id, TravelCreateDTO travel)
        {
            var travelToUpdate = await this.GetTravelAsync(id);

            if (travel.DepartureTime != default(DateTime))
            {
                travelToUpdate.DepartureTime = travel.DepartureTime;
            }

            if (travel.FreeSpots > 0)
            {
                travelToUpdate.FreeSpots = travel.FreeSpots;
            }

            this.dbContext.Update(travelToUpdate);
            await this.dbContext.SaveChangesAsync();

            return this.mapper.Map<TravelPresentDTO>(travelToUpdate);
        }

        public async Task DeleteAsync(int id)
        {
            var travel = await GetTravelAsync(id);
            this.dbContext.Remove(travel);
            await this.dbContext.SaveChangesAsync();
        }

        public IEnumerable<TravelPresentDTO> SearchAllTravels(string startCity, string destinationCity, string driverName, int? freeSpots, bool sortByDate, bool sortByFreeSpots)
        {
            var travels = this.TravelsQuery;
            var searchResult = this.SearchTravels(travels, startCity, destinationCity, driverName, freeSpots, sortByDate, sortByFreeSpots);

            return searchResult.Select(travel => this.mapper.Map<TravelPresentDTO>(travel));
        }

        public IEnumerable<TravelPresentDTO> SearchAvailableTravels(string user, string destinationCity, string startCity, string driverName, int? freeSpots, bool sortByDate, bool sortByFreeSpots)
        {
            var travels = this.TravelsQuery.Where(travel => travel.Driver.Username != user && travel.IsCompleted == false);
            var searchResult = this.SearchTravels(travels, startCity, destinationCity, driverName, freeSpots, sortByDate, sortByFreeSpots);

            return searchResult.Select(travel => this.mapper.Map<TravelPresentDTO>(travel));
        }

        public IEnumerable<TravelPresentDTO> SearchFinishedUserTravels(string user, string startCity, string destinationCity, string driverName, int? freeSpots, bool sortByDate, bool sortByFreeSpots)
        {
            var travelsAsPassenger = this.TravelsQuery.Where(travel => travel.Driver.Username == user && travel.IsCompleted == true);
            var travelsAsDriver = this.TravelsQuery.Where(travel => travel.Passengers.Any(passenger => passenger.Username == user));
            travelsAsDriver.Where(travel => travel.IsCompleted == true);
            var travels = travelsAsPassenger.Union(travelsAsDriver);
            var searchResult = this.SearchTravels(travels, startCity, destinationCity, driverName, freeSpots, sortByDate, sortByFreeSpots);

            return searchResult.Select(travel => this.mapper.Map<TravelPresentDTO>(travel));
        }

        public IEnumerable<TravelPresentDTO> SearchUserAsDriverTravels(string user, string startCity, string destinationCity, int? freeSpots, bool sortByDate, bool sortByFreeSpots)
        {
            var travels = this.TravelsQuery.Where(travel => travel.Driver.Username == user && travel.IsCompleted == false);
            var searchResult = this.SearchTravels(travels, startCity, destinationCity, freeSpots, sortByDate, sortByFreeSpots);

            return searchResult.Select(travel => this.mapper.Map<TravelPresentDTO>(travel));
        }

        public IEnumerable<TravelPresentDTO> SearchAppliedUserTravels(string user, string startCity, string destinationCity, string driverName, int? freeSpots, bool sortByDate, bool sortByFreeSpots)
        {
            var travels = this.TravelsQuery.Where(travel => travel.ApplyingPassengers.Any(passenger => passenger.Username == user));
            var searchResult = this.SearchTravels(travels, startCity, destinationCity, driverName, freeSpots, sortByDate, sortByFreeSpots);

            return searchResult.Select(travel => this.mapper.Map<TravelPresentDTO>(travel));
        }

        public IEnumerable<TravelPresentDTO> SearchApprovedUserTravels(string user, string startCity, string destinationCity, string driverName, int? freeSpots, bool sortByDate, bool sortByFreeSpots)
        {
            var travels = this.TravelsQuery.Where(travel => travel.Passengers.Any(passenger => passenger.Username == user));
            var searchResult = this.SearchTravels(travels, startCity, destinationCity, driverName, freeSpots, sortByDate, sortByFreeSpots);

            return searchResult.Select(travel => this.mapper.Map<TravelPresentDTO>(travel));
        }

        private IQueryable<Travel> SearchTravels(IQueryable<Travel> travels, string startCity, string destinationCity, string driver, int? freeSpots, bool sortByDate, bool sortByFreeSpots)
        {
            if (!string.IsNullOrEmpty(driver))
            {
                travels = travels.Where(travel => travel.Driver.Username.Contains(driver));
            }

            travels = this.SearchTravels(travels, startCity, destinationCity, freeSpots, sortByDate, sortByFreeSpots);

            return travels;
        }

        private IQueryable<Travel> SearchTravels(IQueryable<Travel> travels, string startCity, string destinationCity, int? freeSpots, bool sortByDate, bool sortByFreeSpots)
        {
            if (!string.IsNullOrEmpty(startCity))
            {
                travels = travels.Where(travel => travel.StartPointCity.Name.Contains(startCity));
            }

            if (!string.IsNullOrEmpty(destinationCity))
            {
                travels = travels.Where(travel => travel.EndPointCity.Name.Contains(destinationCity));
            }

            if (freeSpots != null)
            {
                travels = travels.Where(travel => travel.FreeSpots == freeSpots);
            }

            if (sortByDate)
            {
                travels = travels.OrderByDescending(travel => travel.DepartureTime);
            }

            if (sortByFreeSpots)
            {
                travels = travels.OrderByDescending(travel => travel.FreeSpots);
            }

            return travels;
        }

        public async Task ApplyAsPassengerAsync(int userId, int travelId)
        {
            var travel = await this.GetTravelAsync(travelId);

            if (travel.DriverId == userId)
            {
                throw new TravelException($"A driver cannot join his own travel.");
            }

            if (travel.ApplyingPassengers.Any(passenger => passenger.Id == userId))
            {
                throw new EntityAlreadyExistsException($"You are already applied to this travel.");
            }

            this.IsTravelCompleted(travel);
            var user = this.dbContext.Users.FirstOrDefault(user => user.Id == userId);
            if (user != null)
            {

                travel.ApplyingPassengers.Add(user);
                this.dbContext.Travels.Update(travel);
                await this.dbContext.SaveChangesAsync();
            }
            else
            {
                throw new EntityNotFoundException($"USer with ID {userId} not found");
            }
        }

        public async Task AddPassengerAsync(int userId, int driverId, int travelId)
        {
            var travel = await this.GetTravelAsync(travelId);

            this.IsTravelCompleted(travel);
            if (travel.FreeSpots <= 0)
            {
                throw new TravelException($"No free spots in travel with ID {travel.Id}");
            }

            await this.IsDriverAssignedAsync(travelId, driverId);
            var passenger = this.dbContext.Users.FirstOrDefault(user => user.Id == userId);
            if (passenger != null)
            {
                if (travel.ApplyingPassengers.Any(applicant => applicant == passenger))
                {
                    travel.Passengers.Add(passenger);
                    travel.ApplyingPassengers.Remove(passenger);
                    travel.FreeSpots -= 1;
                    await this.dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new EntityNotFoundException($"User with ID {userId} not found in appliyng passengers to travel with ID {travel.Id}.");
                }
            }
            else
            {
                throw new EntityNotFoundException($"User with ID {userId}  not found.");
            }

        }

        public async Task MarkAsCompleteAsync(int travelId)
        {
            var travel = await this.GetTravelAsync(travelId);
            this.IsTravelCompleted(travel);
            travel.IsCompleted = true;
            this.dbContext.Travels.Update(travel);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task CancelTripAsync(int travelId)
        {
            var travel = await this.GetTravelAsync(travelId);
            this.IsTravelCompleted(travel);
            this.dbContext.Travels.Remove(travel);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task RejectPassengerAsync(int userId, int driverId, int travelId)
        {
            await this.IsDriverAssignedAsync(travelId, driverId);
            await this.RemovePassengerAsync(userId, travelId);
        }

        public async Task CancelParticipationAsync(int passengerId, int travelId)
        {
            await this.RemovePassengerAsync(passengerId, travelId);
        }

        private async Task RemovePassengerAsync(int userId, int travelId)
        {
            var travel = await this.GetTravelAsync(travelId);
            this.IsTravelCompleted(travel);

            var applyingPassenger = travel.ApplyingPassengers.FirstOrDefault(applicant => applicant.Id == userId);

            if (applyingPassenger != null)
            {
                travel.ApplyingPassengers.Remove(applyingPassenger);
                await this.dbContext.SaveChangesAsync();
                return;
            }

            var passenger = travel.Passengers.FirstOrDefault(x => x.Id == userId);

            if (passenger != null)
            {
                travel.Passengers.Remove(passenger);
                travel.FreeSpots += 1;
                await this.dbContext.SaveChangesAsync();
                return;
            }
            
            throw new EntityNotFoundException($"User not found.");
        }

        private async Task<bool> IsDriverAssignedAsync(int travelId, int userId)
        {
            var travel = await this.GetTravelAsync(travelId);
            var driverId = travel.DriverId;

            if (userId == driverId)
            {
                return true;
            }
            else
            {
                throw new TravelException($"User with ID {userId} is not assigned as driver to travel with ID {travelId}");
            }
        }

        private void IsTravelCompleted(Travel travel)
        {
            if (travel.IsCompleted == true)
            {
                throw new TravelException($"Travel with ID {travel.Id} is completed");
            }
        }

        private async Task<Travel> GetTravelAsync(int id)
        {
            var travel = await this.TravelsQuery.FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (travel != null)
            {
                return travel;
            }
            else
            {
                throw new EntityNotFoundException($"Travel with ID {id} not found.");
            }
        }
    }
}