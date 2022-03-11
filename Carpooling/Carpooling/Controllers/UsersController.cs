using AutoMapper;
using Carpooling.Services.DTOs;
using Carpooling.Services.Services.Contracts;
using Carpooling.Web.Helpers;
using Carpooling.Web.Helpers.Contracts;
using Carpooling.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Carpooling.Web.Controllers
{
    [AuthorizationAttribute(Role = "User")]
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        private readonly IImageHelper imageHelper;
        private readonly IMapper mapper;

        public UsersController(IUserService userService, IImageHelper imageHelper, IMapper mapper)
        {
            this.userService = userService;
            this.imageHelper = imageHelper;
            this.mapper = mapper;
        }

        public async Task<IActionResult> MyProfile(int id)
        {
            var user = await this.userService.GetAsync(id);
            var result=this.mapper.Map<UserProfileViewModel>(user);
            return View(result);
        }

        public async Task<IActionResult> Update(int id)
        {
            var user = await this.userService.GetAsync(id);
            var result = this.mapper.Map<UserUpdateViewModel>(user);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateViewModel userUpdateViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(userUpdateViewModel);
            }

            string password = userUpdateViewModel.Password;
            if (password != null && !Regex.IsMatch(password, @"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=-]).*$"))
            {
                this.ModelState.AddModelError("PasswordError",
                    "Password must be at least 8 characters long and must contain at least one capital letter, one digit and one special symbol like ( !, *, @, #, $, %, ^, &, +, =, - )");
                return this.View(userUpdateViewModel);
            }

            string profilePicture = string.Empty;
            if (userUpdateViewModel.IProfilePictureName != null)
            {
                profilePicture = await this.imageHelper.UploadImageAsync(userUpdateViewModel.IProfilePictureName);
                this.HttpContext.Session.SetString("ProfilePictureName", profilePicture);
            }
            userUpdateViewModel.ProfilePictureName = profilePicture;
            var user = this.mapper.Map<UserCreateDTO>(userUpdateViewModel);
            var userId = int.Parse(this.HttpContext.Session.GetString("UserId"));
            await this.userService.UpdateAsync(userId, user);
            return this.RedirectToAction("MyProfile", "Users", new { id = userId });
        }
    }
}