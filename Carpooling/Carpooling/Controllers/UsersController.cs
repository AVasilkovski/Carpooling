using Carpooling.Services.Services.Contracts;
using Carpooling.Web.Helpers;
using Carpooling.Web.Helpers.Contracts;
using Carpooling.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Carpooling.Web.Controllers
{
    [AuthorizationAttribute(Role = "User")]
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        private readonly IImageHelper imageHelper;

        public UsersController(IUserService userService, IImageHelper imageHelper)
        {
            this.userService = userService;
            this.imageHelper = imageHelper;
        }

        public IActionResult MyProfile(int id)
        {
            var user = this.userService.Get(id).ToUserProfileViewModel();
            return View(user);
        }

        public IActionResult Update(int id)
        {
            var user = this.userService.Get(id).ToUserUpdateViewModel();
            return View(user);
        }

        [HttpPost]
        public IActionResult Update(UserUpdateViewModel userUpdateViewModel)
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
            if (userUpdateViewModel.ProfilePicture != null)
            {
                profilePicture = this.imageHelper.UploadImage(userUpdateViewModel.ProfilePicture);
                this.HttpContext.Session.SetString("ProfilePicture", profilePicture);
            }

            var user = userUpdateViewModel.ToUserUpdate(profilePicture);
            var userId = int.Parse(this.HttpContext.Session.GetString("UserId"));
            this.userService.Update(userId, user);
            return this.RedirectToAction("MyProfile", "Users", new { id = userId });
        }
    }
}