using AutoMapper;
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
        private readonly IMapper mapper;

        public AdminController(IUserService userService, ITravelService travelService, IMapper mapper)
        {
            this.userService = userService;
            this.travelService = travelService;
            this.mapper = mapper;
        }

        public IActionResult Users(string username, string email, string phoneNumber)
        {
            ViewData["Username"] = username;
            ViewData["Email"] = email;
            ViewData["PhoneNumber"] = phoneNumber;

            var users = this.userService.GetFilteredUsers(phoneNumber, username, email).Select(user => this.mapper.Map<UserViewModel>(user));

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
            var searchResult = allTravels.Select(travel => this.mapper.Map<TravelViewModel>(travel));
            return View(searchResult);
        }
    }
}
