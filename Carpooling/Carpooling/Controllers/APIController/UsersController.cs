using AutoMapper;
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
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        private readonly IAuthHelper authHelper;
        private readonly IMapper mapper;

        public UsersController(IUserService userService, IAuthHelper authHelper, IMapper mapper)
        {
            this.userService = userService;
            this.authHelper = authHelper;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromHeader] string userName, [FromHeader] string password, int id)
        {
            try
            {
                var user = await this.authHelper.TryGetUserAsync(userName, password);
                if (user.Role.Any(role => role.Equals("User") && user.Id == id))
                {
                    var logedUser = await this.userService.GetAsync(id);
                    var result = this.mapper.Map<UserResponseModel>(logedUser);
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
        public async Task<ActionResult> GetUsers([FromHeader] string credentials, [FromHeader] string password)
        {
            try
            {
                var employee = await this.authHelper.TryGetUserAsync(credentials, password);
                if (employee.Role.Any(role => role.Equals("Admin")))
                {
                    var result = this.userService.GetAll().Select(user => this.mapper.Map<UserResponseModel>(user));
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
        public async Task<IActionResult> CreateUser([FromBody] UserRequestModel userRequestModel)//
        {
            try
            {
                var user = this.mapper.Map<UserCreateDTO>(userRequestModel);
                var createdUser = await this.userService.CreateAsync(user);
                var result = this.mapper.Map<UserResponseModel>(createdUser);
                return this.Created("New Customer", result);
            }
            catch (ArgumentNullException)
            {
                return this.BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromHeader] string credentials, [FromHeader] string password, [FromBody] UserRequestModel userRequestModel, int userId)
        {
            try
            {
                var user = await this.authHelper.TryGetUserAsync(credentials, password);
                if (user.Role.Any(role => role.Equals("User") && user.Id == userId))
                {
                    var userToBeModified = this.mapper.Map<UserCreateDTO>(userRequestModel);
                    var updatedUser = await this.userService.UpdateAsync(userId, userToBeModified);
                    var result = this.mapper.Map<UserResponseModel>(updatedUser);
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
        public async Task<IActionResult> DeleteUser([FromHeader] string userName, [FromHeader] string password, int id)
        {
            try
            {
                var user = await this.authHelper.TryGetUserAsync(userName, password);
                if (user.Role.Any(role => role.Equals("Admin") && user.Id == id))
                {
                    await this.userService.DeleteAsync(id);
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
        public async Task<IActionResult> BlockUser([FromHeader] string userName, [FromHeader] string password, int userId)
        {
            try
            {
                var user = await this.authHelper.TryGetUserAsync(userName, password);
                if (user.Role.Contains("Admin") && user.Id != userId && !this.userService.Get(userId).Role.Contains("Admin"))
                {
                    await this.userService.BlockUserAsync(userId);
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
        public async Task<IActionResult> UnblockUser([FromHeader] string userName, [FromHeader] string password, int userId)
        {
            try
            {
                var user = await this.authHelper.TryGetUserAsync(userName, password);
                if (user.Role.Contains("Admin") && user.Id != userId && !this.userService.Get(userId).Role.Contains("Admin"))
                {
                    await this.userService.UnblockUserAsync(userId);
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
        public async Task<IActionResult> SearchAsAdmin([FromHeader] string userName, [FromHeader] string password, string phoneNumber, string usersName, string email)
        {
            try
            {
                var user = await this.authHelper.TryGetUserAsync(userName, password);
                if (user.Role.Any(role => role.Equals("Admin")))
                {
                    var sortedUsers = this.userService.GetFilteredUsers(email, phoneNumber, usersName).Select(user => this.mapper.Map<UserResponseModel>(user));
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
