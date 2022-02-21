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
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        private readonly IAuthHelper authHelper;

        public UsersController(IUserService userService, IAuthHelper authHelper)
        {
            this.userService = userService;
            this.authHelper = authHelper;
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromHeader] string userName, [FromHeader] string password, int id)
        {
            try
            {
                var user = this.authHelper.TryGetUser(userName, password);
                if (user.Roles.Any(role => role == "User" && user.Id == id))
                {
                    var logedUser = this.userService.Get(id);
                    var result = logedUser.UserResponseModel();
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
        public ActionResult GetUsers([FromHeader] string credentials, [FromHeader] string password)
        {
            try
            {
                var employee = this.authHelper.TryGetUser(credentials, password);
                if (employee.Roles.Any(role => role == "Admin"))
                {
                    var result = this.userService.GetAll().Select(user => user.UserResponseModel());
                    return this.Ok(result);
                }

                return this.BadRequest();
            }
            catch (ArgumentException e)
            {
                return this.NotFound(e);
            }
        }

        [HttpPost("")]
        public IActionResult CreateUser([FromBody] UserRequestModel userRequestModel)//
        {
            try
            {
                var user = userRequestModel.UserRequestModel();
                var result = this.userService.Create(user).UserResponseModel();
                return this.Created("New Customer", result);
            }
            catch (ArgumentNullException)
            {
                return this.BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser([FromHeader] string credentials, [FromHeader] string password, [FromBody] UserRequestModel userRequestModel, int userId)
        {
            try
            {
                var user = this.authHelper.TryGetUser(credentials, password);
                if (user.Roles.Any(role => role == "User" && user.Id == userId))
                {
                    var userToBeModified = userRequestModel.UserRequestModel();
                    var updatedUser = this.userService.Update(userId, userToBeModified);
                    var result = updatedUser.UserResponseModel();
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
        public IActionResult DeleteUser([FromHeader] string userName, [FromHeader] string password, int id)
        {
            try
            {
                var user = this.authHelper.TryGetUser(userName, password);
                if (user.Roles.Any(role => role == "Admin" && user.Id == id))
                {
                    this.userService.Delete(id);
                    return this.NoContent();
                }
                return BadRequest();
            }
            catch (ArgumentException e)
            {
                return this.NotFound(e);
            }
        }

        [HttpPut("{userId}/Block")]
        public IActionResult BlockUser([FromHeader] string userName, [FromHeader] string password, int userId)
        {
            try
            {
                var user = this.authHelper.TryGetUser(userName, password);
                if (user.Roles.Contains("Admin") && user.Id != userId && !this.userService.Get(userId).Roles.Contains("Admin"))
                {
                    this.userService.BlockUser(userId);
                    return this.Ok();
                }
                return this.BadRequest();
            }
            catch (ArgumentException e)
            {
                return this.NotFound(e);
            }
        }

        [HttpPut("{userId}/Unblock")]
        public IActionResult UnblockUser([FromHeader] string userName, [FromHeader] string password, int userId)
        {
            try
            {
                var user = this.authHelper.TryGetUser(userName, password);
                if (user.Roles.Contains("Admin") && user.Id != userId && !this.userService.Get(userId).Roles.Contains("Admin"))
                {
                    this.userService.UnblockUser(userId);
                    return this.Ok();
                }
                return this.BadRequest();
            }
            catch (ArgumentException e)
            {
                return this.NotFound(e);
            }
        }

        [HttpGet("Search")]
        public IActionResult SearchAsAdmin([FromHeader] string userName, [FromHeader] string password, string phoneNumber, string usersName, string email)
        {
            try
            {
                var user = this.authHelper.TryGetUser(userName, password);
                if (user.Roles.Any(role => role == "Admin"))
                {
                    var sortedUsers = this.userService.GetFilteredUsers(email, phoneNumber, usersName).Select(user => user.UserResponseModel());
                    return this.Ok(sortedUsers);
                }
                return BadRequest();
            }
            catch (ArgumentException e)
            {
                return this.NotFound(e);
            }
        }

    }
}
