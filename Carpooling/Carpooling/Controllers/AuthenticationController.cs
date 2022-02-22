using Carpooling.Services.Exceptions;
using Carpooling.Services.Services.Contracts;
using Carpooling.Web.Helpers.Contracts;
using Carpooling.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Carpooling.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserService userService;
        private readonly IAuthHelper authHelper;

        public AuthenticationController(IUserService userService, IAuthHelper authHelper)
        {
            this.userService = userService;
            this.authHelper = authHelper;
        }

        public IActionResult Login()
        {
            var loginVm = new LoginViewModel();

            return View(loginVm);
        }

        [HttpPost]
        public IActionResult Login([Bind("Username", "Password")] LoginViewModel loginViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(loginViewModel);
            }

            try
            {
                var user = this.authHelper.TryGetUser(loginViewModel.Username, loginViewModel.Password);
                this.HttpContext.Session.SetString("CurrentUser", user.Username);
                this.HttpContext.Session.SetString("CurrentRoles", string.Join(',', user.Roles));
                this.HttpContext.Session.SetString("UserId", user.Id.ToString());
                this.HttpContext.Session.SetString("ProfilePicture", user.ProfilePic);

                return this.RedirectToAction("index", "home");
            }
            catch (EntityNotFoundException e)
            {
                this.ModelState.AddModelError("LogInError", e.Message);
                return this.View(loginViewModel);
            }
        }

        public IActionResult Logout()
        {
            this.HttpContext.Session.Clear();
            return this.RedirectToAction("index", "home");
        }

        public IActionResult Register()
        {
            var registerViewModel = new RegisterViewModel();

            return View(registerViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(registerViewModel);
            }

            try
            {
                this.userService.IsUserUnique(registerViewModel.Username, registerViewModel.Email, registerViewModel.PhoneNumber);
            }
            catch (EntityAlreadyExistsException e)
            {
                this.ModelState.AddModelError("RegisterError", e.Message);
                return this.View(registerViewModel);
            }

            var user = registerViewModel.ToUserCreateDTO();
            await this.userService.CreateAsync(user);
            return this.RedirectToAction(nameof(this.Login));
        }
    }
}