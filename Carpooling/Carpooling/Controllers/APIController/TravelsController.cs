using AutoMapper;
using Carpooling.Data.Models.Enums;
using Carpooling.Services.DTOs;
using Carpooling.Services.Services.Contracts;
using Carpooling.Web.Helpers.Contracts;
using Carpooling.Web.Models.APIModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Carpooling.Web.Controllers.APIController
{
    [ApiController]
    [Route("api/[controller]")]
    public class TravelsController : ControllerBase
    {
        private readonly ITravelService travelService;
        private readonly IAuthHelper authHelper;
        private readonly IMapper mapper;

        public TravelsController(ITravelService travelService, IAuthHelper authHelper, IMapper mapper)
        {
            this.travelService = travelService;
            this.authHelper = authHelper;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromHeader] string userName, [FromHeader] string password, int id)
        {
            try
            {
                var user = await this.authHelper.TryGetUserAsync(userName, password);
                if (user.Role.Any(role => role.Equals("User")))
                {
                    var travel = await this.travelService.GetAsync(id);
                    var result = this.mapper.Map<TravelResponseModel>(travel);
                    return Ok(result);
                }
                return BadRequest();
            }
            catch (ArgumentException e)
            {
                return this.NotFound(e);
            }
        }

        [HttpGet("")]
        public async Task<IActionResult> Get([FromHeader] string userName, [FromHeader] string password)
        {
            try
            {
                var user = await this.authHelper.TryGetUserAsync(userName, password);
                if (user.Role.Any(role => role.Equals("Admin")))
                {
                    var travels = this.travelService.GetAll().Select(travel => this.mapper.Map<TravelResponseModel>(travel));
                    return Ok(travels);
                }
                return BadRequest();
            }
            catch (ArgumentException e)
            {
                return this.NotFound(e);
            }
        }
        
        [HttpGet("{username}/Approved")]
        public async Task<IActionResult> SearchApprovedTravels([FromHeader] string userName, [FromHeader] string password, string destinationCity, string startCity, string driverName,
                                               int? freeSpots, bool sortByDate, bool sortByFreeSpots)
        {
            try
            {
                var user = await this.authHelper.TryGetUserAsync(userName, password);
                if (user.Role.Any(role => role.Equals("User")))
                {
                    var travels = this.travelService.SearchApprovedUserTravels(user.Username, startCity, destinationCity, driverName, freeSpots, sortByDate, sortByFreeSpots);
                    var searchResult = travels.Select(travel => this.mapper.Map<TravelResponseModel>(travel));
                    return this.Ok(searchResult);
                }
                return BadRequest();
            }
            catch (ArgumentException e)
            {
                return this.NotFound(e);
            }
        }
        
        [HttpGet("{username}/Applied")]
        public async Task<IActionResult> SortAppliedTravels([FromHeader] string userName, [FromHeader] string password, string destinationCity, string startCity, string driverName,
                                             int? freeSpots, bool sortByDate, bool sortByFreeSpots)
        {
            try
            {
                var user = await this.authHelper.TryGetUserAsync (userName, password);
                if (user.Role.Any(role => role.Equals("User")))
                {
                    var travels = this.travelService.SearchAppliedUserTravels(user.Username, startCity, destinationCity, driverName, freeSpots, sortByDate, sortByFreeSpots);
                    var searchResult = travels.Select(travel => this.mapper.Map<TravelResponseModel>(travel));
                    return this.Ok(searchResult);
                }
                return BadRequest();
            }
            catch (ArgumentException e)
            {
                return this.NotFound(e);
            }
        }
        
        [HttpGet("Drivers/{username}")]
        public async Task<IActionResult> SearchAvailableTravels([FromHeader] string userName, [FromHeader] string password, string destinationCity, string startCity, int? freeSpots,
                                                    bool sortByDate, bool sortByFreeSpots)
        {
            try
            {
                var user = await this.authHelper.TryGetUserAsync(userName, password);
                if (user.Role.Any(role => role.Equals("User")))
                {
                    var travels = this.travelService.SearchUserAsDriverTravels(user.Username, startCity, destinationCity, freeSpots, sortByDate, sortByFreeSpots);
                    var searchResult = travels.Select(travel => this.mapper.Map<TravelResponseModel>(travel));
                    return this.Ok(searchResult);
                }
                return BadRequest();
            }
            catch (ArgumentException e)
            {
                return this.NotFound(e);
            }
        }
       
        [HttpGet("{username}/Finished")]
        public async Task<IActionResult> SortFinishedTravels([FromHeader] string userName, [FromHeader] string password, string destinationCity, string startCity, string driverName, int? freeSpots, bool sortByDate, bool sortByFreeSpots)
        {
            try
            {
                var user = await this.authHelper.TryGetUserAsync(userName, password);
                if (user.Role.Any(role => role.Equals("User")))
                {
                    var travels = this.travelService.SearchFinishedUserTravels(user.Username, startCity, destinationCity, driverName, freeSpots, sortByDate, sortByFreeSpots);
                    var searchResult = travels.Select(travel => this.mapper.Map<TravelResponseModel>(travel));
                    return this.Ok(searchResult);
                }
                return BadRequest();
            }
            catch (ArgumentException e)
            {
                return this.NotFound(e);
            }
        }
        
        [HttpGet("Search")]
        public async Task<IActionResult> SearchAsAdmin([FromHeader] string userName, [FromHeader] string password, string destinationCity, string startCity, string driverName, int? freeSpots, bool sortByDate, bool sortByFreeSpots)
        {
            try
            {
                var user = await this.authHelper.TryGetUserAsync(userName, password);
                if (user.Role.Any(role => role.Equals("Admin")))
                {
                    var travels = this.travelService.SearchAllTravels(startCity, destinationCity, driverName, freeSpots, sortByDate, sortByFreeSpots);
                    var searchResult = travels.Select(travel => this.mapper.Map<TravelResponseModel>(travel));
                    return this.Ok(searchResult);
                }
                return BadRequest();
            }
            catch (ArgumentException e)
            {
                return this.NotFound(e);
            }
        }
        
        [HttpGet("Available")]
        public async Task<IActionResult> SortAsAdmin([FromHeader] string userName, [FromHeader] string password, string destinationCity, string startCity, string driverName, int? freeSpots, bool sortByDate, bool sortByFreeSpots)
        {
            try
            {
                var user = await this.authHelper.TryGetUserAsync(userName, password);
                if (user.Role.Any(role => role.Equals("User")))
                {
                    var travels = this.travelService.SearchAvailableTravels(user.Username, startCity, destinationCity, driverName, freeSpots, sortByDate, sortByFreeSpots);
                    var searchResult = travels.Select(travel => this.mapper.Map<TravelResponseModel>(travel));
                    return this.Ok(searchResult);
                }
                return BadRequest();
            }
            catch (ArgumentException e)
            {
                return this.NotFound(e);
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateTravel([FromHeader] string userName, [FromHeader] string password, [FromBody] TravelRequestModel travelToBeCreated)
        {
            try
            {
                var user = await this.authHelper.TryGetUserAsync(userName, password);
                if (user.Role.Any(role => role.Equals("User")) && user.UserStatus == UserStatus.Active)
                {
                    var travel = this.mapper.Map<TravelCreateDTO>(travelToBeCreated);
                    var createdTravel = await this.travelService.CreateAsync(travel); 
                    var result = this.mapper.Map<TravelResponseModel>(createdTravel);

                    return this.Created("New Travel", result);
                }
                return this.BadRequest();
            }
            catch (ArgumentNullException e)
            {
                return this.NotFound(e);
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateTravel([FromHeader] string userName, [FromHeader] string password, int travelId, [FromBody] TravelRequestModel travelToUpdate)
        {
            try
            {
                var user = await this.authHelper.TryGetUserAsync(userName, password);
                if (user.Role.Any(role => role.Equals("User")))
                {
                    var travelDto = this.mapper.Map<TravelCreateDTO>(travelToUpdate);
                    var updatedTravel = await this.travelService.UpdateAsync(travelId, travelDto);
                    var result = this.mapper.Map<TravelResponseModel>(updatedTravel);

                    return this.Ok(result);
                }
                return this.BadRequest();
            }
            catch (ArgumentException e)
            {
                return this.NotFound(e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTravel([FromHeader] string userName, [FromHeader] string password, int id)
        {
            try
            {
                var user = await this.authHelper.TryGetUserAsync(userName, password);
                if (user.Role.Any(role => role.Equals("Admin")))
                {
                    await this.travelService.DeleteAsync(id);
                    return this.NoContent();
                }
                return BadRequest();
            }
            catch (ArgumentException e)
            {
                return this.NotFound(e);
            }
        }

        [HttpPut("{travelId}/Passenger/{userId}")]
        public async Task<IActionResult> AddPassenger([FromHeader] string userName, [FromHeader] string password, int userId, int travelId)
        {
            try
            {
                var user = await this.authHelper.TryGetUserAsync(userName, password);
                if (user.Role.Any(role => role.Equals("User")) && user.UserStatus == UserStatus.Active)
                {
                    await this.travelService.AddPassengerAsync(userId, user.Id, travelId);
                    return this.Ok();
                }
                return this.BadRequest();
            }
            catch (ArgumentException e)
            {
                return this.NotFound(e);
            }
        }

        [HttpPut("Complete/{id}")]
        public async Task<IActionResult> MarkAsComplete([FromHeader] string userName, [FromHeader] string password, int id)
        {
            try
            {
                var user = await this.authHelper.TryGetUserAsync(userName, password);
                if (user.Role.Any(role => role.Equals("User")))
                {
                    await this.travelService.MarkAsCompleteAsync(id);
                    return this.Ok();
                }
                return this.BadRequest();
            }
            catch (ArgumentException e)
            {
                return this.NotFound(e);
            }
        }

        [HttpDelete("Cancel/{id}")]
        public async Task<IActionResult> CancelTrip([FromHeader] string userName, [FromHeader] string password, int id)
        {
            try
            {
                var user = await this.authHelper.TryGetUserAsync(userName, password);
                if (user.Role.Any(role => role.Equals("User")))
                {
                    await this.travelService.CancelTripAsync(id);
                    return this.Ok();
                }
                return this.BadRequest();
            }
            catch (ArgumentException e)
            {
                return this.NotFound(e);
            }
        }

        [HttpPut("{travelId}/Apply/{userName}")]
        public async Task<IActionResult> ApplyAsPassenger([FromHeader] string userName, [FromHeader] string password, int travelId)
        {
            try
            {
                var user = await this.authHelper.TryGetUserAsync(userName, password);
                if (user.Role.Any(role => role.Equals("User")) && user.UserStatus == UserStatus.Active)
                {
                    await this.travelService.ApplyAsPassengerAsync(user.Id, travelId);
                    return this.Ok();
                }
                return this.BadRequest();
            }
            catch (ArgumentException e)
            {
                return this.NotFound(e);
            }
        }

        [HttpPut("{travelId}/Reject/{userName}")]
        public async Task<IActionResult> RejectPassenger([FromHeader] string userName, [FromHeader] string password, int userId, int travelId)
        {
            try
            {
                var user = await this.authHelper.TryGetUserAsync(userName, password);
                if (user.Role.Any(role => role.Equals("User")))
                {
                    await this.travelService.RejectPassengerAsync(userId, user.Id, travelId);
                    return this.Ok();
                }
                return this.BadRequest();
            }
            catch (ArgumentException e)
            {
                return this.NotFound(e);
            }
        }

        [HttpPut("{travelId}/Cancel/{userName}")]

        public async Task<IActionResult> CancelParticipation([FromHeader] string userName, [FromHeader] string password, int travelId)
        {
            try
            {
                var user = await this.authHelper.TryGetUserAsync(userName, password);
                if (user.Role.Any(role => role.Equals("User")))
                {
                    await this.travelService.CancelParticipationAsync(user.Id, travelId);
                    return this.Ok($"Participation in travel with ID {travelId} canceled");
                }
                return this.BadRequest();
            }
            catch (ArgumentException e)
            {
                return this.NotFound(e);
            }
        }
    }
}
