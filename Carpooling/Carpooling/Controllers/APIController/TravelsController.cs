using Carpooling.Data.Models.Enums;
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

        public TravelsController(ITravelService travelService, IAuthHelper authHelper)
        {
            this.travelService = travelService;
            this.authHelper = authHelper;
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromHeader] string userName, [FromHeader] string password, int id)
        {
            try
            {
                var user = this.authHelper.TryGetUser(userName, password);
                if (user.Roles.Any(role => role == "User"))
                {
                    var travel = this.travelService.Get(id);
                    var result = travel.TravelResponseModel();
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
        public IActionResult Get([FromHeader] string userName, [FromHeader] string password)
        {
            try
            {
                var user = this.authHelper.TryGetUser(userName, password);
                if (user.Roles.Any(role => role == "Admin"))
                {
                    var travels = this.travelService.GetAll().Select(travel => travel.TravelResponseModel());
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
        public IActionResult SearchApprovedTravels([FromHeader] string userName, [FromHeader] string password, string destinationCity, string startCity, string driverName,
                                               int? freeSpots, bool sortByDate, bool sortByFreeSpots)
        {
            try
            {
                var user = this.authHelper.TryGetUser(userName, password);
                if (user.Roles.Any(role => role == "User"))
                {
                    var travels = this.travelService.SearchApprovedUserTravels(user.Username, startCity, destinationCity, driverName, freeSpots, sortByDate, sortByFreeSpots);
                    var searchResult = travels.Select(travel => travel.TravelResponseModel());
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
        public IActionResult SortAppliedTravels([FromHeader] string userName, [FromHeader] string password, string destinationCity, string startCity, string driverName,
                                             int? freeSpots, bool sortByDate, bool sortByFreeSpots)
        {
            try
            {
                var user = this.authHelper.TryGetUser(userName, password);
                if (user.Roles.Any(role => role == "User"))
                {
                    var travels = this.travelService.SearchAppliedUserTravels(user.Username, startCity, destinationCity, driverName, freeSpots, sortByDate, sortByFreeSpots);
                    var searchResult = travels.Select(travel => travel.TravelResponseModel());
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
        public IActionResult SearchAvailableTravels([FromHeader] string userName, [FromHeader] string password, string destinationCity, string startCity, int? freeSpots,
                                                    bool sortByDate, bool sortByFreeSpots)
        {
            try
            {
                var user = this.authHelper.TryGetUser(userName, password);
                if (user.Roles.Any(role => role == "User"))
                {
                    var travels = this.travelService.SearchUserAsDriverTravels(user.Username, startCity, destinationCity, freeSpots, sortByDate, sortByFreeSpots);
                    var searchResult = travels.Select(travel => travel.TravelResponseModel());
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
        public IActionResult SortFinishedTravels([FromHeader] string userName, [FromHeader] string password, string destinationCity, string startCity, string driverName, int? freeSpots, bool sortByDate, bool sortByFreeSpots)
        {
            try
            {
                var user = this.authHelper.TryGetUser(userName, password);
                if (user.Roles.Any(role => role == "User"))
                {
                    var travels = this.travelService.SearchFinishedUserTravels(user.Username, startCity, destinationCity, driverName, freeSpots, sortByDate, sortByFreeSpots);
                    var searchResult = travels.Select(travel => travel.TravelResponseModel());
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
        public IActionResult SearchAsAdmin([FromHeader] string userName, [FromHeader] string password, string destinationCity, string startCity, string driverName, int? freeSpots, bool sortByDate, bool sortByFreeSpots)
        {
            try
            {
                var user = this.authHelper.TryGetUser(userName, password);
                if (user.Roles.Any(role => role == "Admin"))
                {
                    var travels = this.travelService.SearchAllTravels(startCity, destinationCity, driverName, freeSpots, sortByDate, sortByFreeSpots);
                    var searchResult = travels.Select(travel => travel.TravelResponseModel());
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
        public IActionResult SortAsAdmin([FromHeader] string userName, [FromHeader] string password, string destinationCity, string startCity, string driverName, int? freeSpots, bool sortByDate, bool sortByFreeSpots)
        {
            try
            {
                var user = this.authHelper.TryGetUser(userName, password);
                if (user.Roles.Any(role => role == "User"))
                {
                    var travels = this.travelService.SearchAvailableTravels(user.Username, startCity, destinationCity, driverName, freeSpots, sortByDate, sortByFreeSpots);
                    var searchResult = travels.Select(travel => travel.TravelResponseModel());
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
                var user = this.authHelper.TryGetUser(userName, password);
                if (user.Roles.Any(role => role == "User") && user.Status == UserStatus.Active)
                {
                    var travel = travelToBeCreated.TravelRequestModel();
                    var createdTravel = await this.travelService.CreateAsync(travel); 
                    var result = createdTravel.TravelResponseModel();

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
                var user = this.authHelper.TryGetUser(userName, password);
                if (user.Roles.Any(role => role == "User"))
                {
                    var travelDto = travelToUpdate.TravelRequestModel();
                    var updatedTravel = await this.travelService.UpdateAsync(travelId, travelDto);
                    var result = updatedTravel.TravelResponseModel();

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
                var user = this.authHelper.TryGetUser(userName, password);
                if (user.Roles.Any(role => role == "Admin"))
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
                var user = this.authHelper.TryGetUser(userName, password);
                if (user.Roles.Any(role => role == "User") && user.Status == UserStatus.Active)
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
                var user = this.authHelper.TryGetUser(userName, password);
                if (user.Roles.Any(role => role == "User"))
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
                var user = this.authHelper.TryGetUser(userName, password);
                if (user.Roles.Any(role => role == "User"))
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
                var user = this.authHelper.TryGetUser(userName, password);
                if (user.Roles.Any(role => role == "User") && user.Status == UserStatus.Active)
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
                var user = this.authHelper.TryGetUser(userName, password);
                if (user.Roles.Any(role => role == "User"))
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
                var user = this.authHelper.TryGetUser(userName, password);
                if (user.Roles.Any(role => role == "User"))
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
