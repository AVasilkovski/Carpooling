using Carpooling.Data.Models.Enums;
using Carpooling.Services.Services.Contracts;
using Carpooling.Web.Helpers.Contracts;
using Carpooling.Web.Models.APIModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

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

        // api/Travels/{username}/Approved
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

        // api/Travels/{username}/Applied
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

        // api/Travels/Drivers/{username}
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

        // api/Travels/{usename}/Finished
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

        // api/Travels/Search
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

        // api/Travels/Available
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
        public IActionResult CreateTravel([FromHeader] string userName, [FromHeader] string password, [FromBody] TravelRequestModel travelToBeCreated)
        {
            try
            {
                var user = this.authHelper.TryGetUser(userName, password);
                if (user.Roles.Any(role => role == "User") && user.Status == UserStatus.Active)
                {
                    var travel = travelToBeCreated.TravelRequestModel();
                    var result = this.travelService.Create(travel).TravelResponseModel();
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
        public IActionResult UpdateTravel([FromHeader] string userName, [FromHeader] string password, int travelId, [FromBody] TravelRequestModel travelToUpdate)
        {
            try
            {
                var user = this.authHelper.TryGetUser(userName, password);
                if (user.Roles.Any(role => role == "User"))
                {
                    var travelDto = travelToUpdate.TravelRequestModel();
                    var result = this.travelService.Update(travelId, travelDto).TravelResponseModel();

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
        public IActionResult DeleteTravel([FromHeader] string userName, [FromHeader] string password, int id)
        {
            try
            {
                var user = this.authHelper.TryGetUser(userName, password);
                if (user.Roles.Any(role => role == "Admin"))
                {
                    this.travelService.Delete(id);
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
        public IActionResult AddPassenger([FromHeader] string userName, [FromHeader] string password, int userId, int travelId)
        {
            try
            {
                var user = this.authHelper.TryGetUser(userName, password);
                if (user.Roles.Any(role => role == "User") && user.Status == UserStatus.Active)
                {
                    this.travelService.AddPassenger(userId, user.Id, travelId);
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
        public IActionResult MarkAsComplete([FromHeader] string userName, [FromHeader] string password, int id)
        {
            try
            {
                var user = this.authHelper.TryGetUser(userName, password);
                if (user.Roles.Any(role => role == "User"))
                {
                    this.travelService.MarkAsComplete(id);
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
        public IActionResult CancelTrip([FromHeader] string userName, [FromHeader] string password, int id)
        {
            try
            {
                var user = this.authHelper.TryGetUser(userName, password);
                if (user.Roles.Any(role => role == "User"))
                {
                    this.travelService.CancelTrip(id);
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
        public IActionResult ApplyAsPassenger([FromHeader] string userName, [FromHeader] string password, int travelId)
        {
            try
            {
                var user = this.authHelper.TryGetUser(userName, password);
                if (user.Roles.Any(role => role == "User") && user.Status == UserStatus.Active)
                {
                    this.travelService.ApplyAsPassenger(user.Id, travelId);
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
        public IActionResult RejectPassenger([FromHeader] string userName, [FromHeader] string password, int userId, int travelId)
        {
            try
            {
                var user = this.authHelper.TryGetUser(userName, password);
                if (user.Roles.Any(role => role == "User"))
                {
                    this.travelService.RejectPassenger(userId, user.Id, travelId);
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

        public IActionResult CancelParticipation([FromHeader] string userName, [FromHeader] string password, int travelId)
        {
            try
            {
                var user = this.authHelper.TryGetUser(userName, password);
                if (user.Roles.Any(role => role == "User"))
                {
                    this.travelService.CancelParticipation(user.Id, travelId);
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
