using Carpooling.Services.Services.Contracts;
using Carpooling.Web.Helpers;
using Carpooling.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Carpooling.Web.Controllers
{
    [AuthorizationAttribute(Role = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserService userService;
        private readonly ITravelService travelService;

        public AdminController(IUserService userService, ITravelService travelService)
        {
            this.userService = userService;
            this.travelService = travelService;
        }

        public IActionResult Users(string username, string email, string phoneNumber)
        {
            ViewData["Username"] = username;
            ViewData["Email"] = email;
            ViewData["PhoneNumber"] = phoneNumber;

            var users = this.userService.GetFilteredUsers(phoneNumber, username, email).Select(user => user.ToUserViewModel());

            return View(users);
        }

        public async Task<IActionResult> Block(int id)
        {
            await this.userService.BlockUserAsync(id);
            return this.RedirectToAction("Users", "admin");
        }


        public async Task<IActionResult> Unblock(int id)
        {
            await this.userService.UnblockUserAsync(id);
            return this.RedirectToAction("Users", "admin");
        }

        public IActionResult Travels(string startCity, string destinationCity, string driver, int? spots, bool dateSort, bool spotsSort)
        {
            ViewData["StartCity"] = startCity;
            ViewData["DestinationCity"] = destinationCity;
            ViewData["Driver"] = driver;
            ViewData["Spots"] = spots;

            var allTravels = this.travelService.SearchAllTravels(startCity, destinationCity, driver, spots, dateSort, spotsSort);
            var searchResult = allTravels.Select(travel => travel.ToTravelViewModel());
            return View(searchResult);
        }
    }
}
