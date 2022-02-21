using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Carpooling.Data;
using Carpooling.Data.Models;
using Carpooling.Services.DTOs;
using Carpooling.Services.Services.Contracts;
using Carpooling.Services.Exceptions;

namespace Carpooling.Services.Services
{
    public class TravelService : ITravelService
    {
        private readonly CarpoolingContext dbContext;
        private readonly ICityService cityService;
        private readonly ITravelTagService travelTagService;

        public TravelService(CarpoolingContext dbContext, ITravelTagService travelTagService, ICityService cityService)
        {
            this.dbContext = dbContext;
            this.travelTagService = travelTagService;
            this.cityService = cityService;
        }

        private IQueryable<Travel> TravelsQuery
        {
            get
            {
                return this.dbContext.Travels.Include(start => start.StartPointCity)
                                               .Include(end => end.EndPointCity)
                                               .Include(driver => driver.Driver)
                                                       .ThenInclude(role => role.Roles)
                                               .Include(passenger => passenger.Passengers)
                                                      .ThenInclude(role => role.Roles)
                                               .Include(applying => applying.ApplyingPassengers)
                                                      .ThenInclude(role => role.Roles)
                                               .Include(feedback => feedback.Feedbacks)
                                               .Include(tag => tag.TravelTags);
            }
        }

        public IEnumerable<TravelPresentDTO> GetAll()
        {
            return this.TravelsQuery.Select(travel => travel.ToTravelDTO());
        }

        public TravelPresentDTO Get(int id)
        {
            var result = this.GetTravel(id).ToTravelDTO();

            return result;
        }

        public TravelPresentDTO Create(TravelCreateDTO newTravel)
        {
            var startCity = this.cityService.CheckIfCityExist(newTravel.StartPointAddress.City);
            var destinationCity = this.cityService.CheckIfCityExist(newTravel.EndPointAddress.City);
            var travel = newTravel.ToTravel();
            travel.StartPointCity = startCity;
            travel.EndPointCity = destinationCity;
            travel.TravelTags = this.travelTagService.FindTags(newTravel.TravelTags);
            this.dbContext.Travels.Add(travel);
            this.dbContext.SaveChanges();
            travel = this.GetTravel(travel.Id);

            return travel.ToTravelDTO();
        }

        public TravelPresentDTO Update(int id, TravelCreateDTO travel)
        {
            var travelToUpdate = this.GetTravel(id);

            if (travel.DepartureTime != default(DateTime))
            {
                travelToUpdate.DepartureTime = travel.DepartureTime;
            }

            if (travel.FreeSpots > 0)
            {
                travelToUpdate.FreeSpots = travel.FreeSpots;
            }

            this.dbContext.Update(travelToUpdate);
            this.dbContext.SaveChanges();

            return travelToUpdate.ToTravelDTO();
        }

        public void Delete(int id)
        {
            var travel = GetTravel(id);
            this.dbContext.Remove(travel);
            this.dbContext.SaveChanges();
        }

        public IEnumerable<TravelPresentDTO> SearchAllTravels(string startCity, string destinationCity, string driverName, int? freeSpots, bool sortByDate, bool sortByFreeSpots)
        {
            var travels = this.TravelsQuery;
            var searchResult = this.SearchTravels(travels, startCity, destinationCity, driverName, freeSpots, sortByDate, sortByFreeSpots);

            return searchResult.Select(travel => travel.ToTravelDTO());
        }

        public IEnumerable<TravelPresentDTO> SearchAvailableTravels(string user, string destinationCity, string startCity, string driverName, int? freeSpots, bool sortByDate, bool sortByFreeSpots)
        {
            var travels = this.TravelsQuery.Where(travel => travel.Driver.Username != user && travel.IsCompleted == false);
            var searchResult = this.SearchTravels(travels, startCity, destinationCity, driverName, freeSpots, sortByDate, sortByFreeSpots);

            return searchResult.Select(travel => travel.ToTravelDTO());
        }

        public IEnumerable<TravelPresentDTO> SearchFinishedUserTravels(string user, string startCity, string destinationCity, string driverName, int? freeSpots, bool sortByDate, bool sortByFreeSpots)
        {
            var travelsAsPassenger = this.TravelsQuery.Where(travel => travel.Driver.Username == user && travel.IsCompleted == true);
            var travelsAsDriver = this.TravelsQuery.Where(travel => travel.Passengers.Any(passenger => passenger.Username == user));
            travelsAsDriver.Where(travel => travel.IsCompleted == true);
            var travels = travelsAsPassenger.Union(travelsAsDriver);
            var searchResult = this.SearchTravels(travels, startCity, destinationCity, driverName, freeSpots, sortByDate, sortByFreeSpots);

            return searchResult.Select(travel => travel.ToTravelDTO());
        }

        public IEnumerable<TravelPresentDTO> SearchUserAsDriverTravels(string user, string startCity, string destinationCity, int? freeSpots, bool sortByDate, bool sortByFreeSpots)
        {
            var travels = this.TravelsQuery.Where(travel => travel.Driver.Username == user && travel.IsCompleted == false);
            var searchResult = this.SearchTravels(travels, startCity, destinationCity, freeSpots, sortByDate, sortByFreeSpots);

            return searchResult.Select(travel => travel.ToTravelDTO());
        }

        public IEnumerable<TravelPresentDTO> SearchAppliedUserTravels(string user, string startCity, string destinationCity, string driverName, int? freeSpots, bool sortByDate, bool sortByFreeSpots)
        {
            var travels = this.TravelsQuery.Where(travel => travel.ApplyingPassengers.Any(passenger => passenger.Username == user));
            var searchResult = this.SearchTravels(travels, startCity, destinationCity, driverName, freeSpots, sortByDate, sortByFreeSpots);

            return searchResult.Select(travel => travel.ToTravelDTO());
        }

        public IEnumerable<TravelPresentDTO> SearchApprovedUserTravels(string user, string startCity, string destinationCity, string driverName, int? freeSpots, bool sortByDate, bool sortByFreeSpots)
        {
            var travels = this.TravelsQuery.Where(travel => travel.Passengers.Any(passenger => passenger.Username == user));
            var searchResult = this.SearchTravels(travels, startCity, destinationCity, driverName, freeSpots, sortByDate, sortByFreeSpots);

            return searchResult.Select(travel => travel.ToTravelDTO());
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

        public void ApplyAsPassenger(int userId, int travelId)
        {
            var travel = this.GetTravel(travelId);

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
                this.dbContext.SaveChanges();
            }
            else
            {
                throw new EntityNotFoundException($"USer with ID {userId} not found");
            }
        }

        public void AddPassenger(int userId, int driverId, int travelId)
        {
            var travel = this.GetTravel(travelId);

            this.IsTravelCompleted(travel);
            if (travel.FreeSpots <= 0)
            {
                throw new TravelException($"No free spots in travel with ID {travel.Id}");
            }

            this.IsDriverAssigned(travelId, driverId);
            var passenger = this.dbContext.Users.FirstOrDefault(user => user.Id == userId);
            if (passenger != null)
            {
                if (travel.ApplyingPassengers.Any(applicant => applicant == passenger))
                {
                    travel.Passengers.Add(passenger);
                    travel.ApplyingPassengers.Remove(passenger);
                    travel.FreeSpots -= 1;
                    this.dbContext.SaveChanges();
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

        public void MarkAsComplete(int travelId)
        {
            var travel = this.GetTravel(travelId);
            this.IsTravelCompleted(travel);
            travel.IsCompleted = true;
            this.dbContext.Travels.Update(travel);
            this.dbContext.SaveChanges();
        }

        public void CancelTrip(int travelId)
        {
            var travel = this.GetTravel(travelId);
            this.IsTravelCompleted(travel);
            this.dbContext.Travels.Remove(travel);
            this.dbContext.SaveChanges();
        }

        public void RejectPassenger(int userId, int driverId, int travelId)
        {
            this.IsDriverAssigned(travelId, driverId);
            this.RemovePassenger(userId, travelId);
        }

        public void CancelParticipation(int passengerId, int travelId)
        {
            this.RemovePassenger(passengerId, travelId);
        }

        private void RemovePassenger(int userId, int travelId)
        {
            var travel = this.GetTravel(travelId);
            this.IsTravelCompleted(travel);

            var applyingPassenger = travel.ApplyingPassengers.FirstOrDefault(applicant => applicant.Id == userId);

            if (applyingPassenger != null)
            {
                travel.ApplyingPassengers.Remove(applyingPassenger);
                this.dbContext.SaveChanges();
                return;
            }

            var passenger = travel.Passengers.FirstOrDefault(x => x.Id == userId);

            if (passenger != null)
            {
                travel.Passengers.Remove(passenger);
                travel.FreeSpots += 1;
                this.dbContext.SaveChanges();
                return;
            }
            
            throw new EntityNotFoundException($"User not found.");
        }

        private bool IsDriverAssigned(int travelId, int userId)
        {
            var travel = this.GetTravel(travelId);
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

        private Travel GetTravel(int id)
        {
            var travel = this.TravelsQuery.FirstOrDefault(x => x.Id.Equals(id));

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