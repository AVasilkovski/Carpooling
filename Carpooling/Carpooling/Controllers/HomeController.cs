using AutoMapper;
using Carpooling.Services.Services.Contracts;
using Carpooling.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace Carpooling.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService userService;
        private readonly ITravelService travelService;
        private readonly IMapper mapper;

        public HomeController(IUserService userService, ITravelService travelService, IMapper mapper)
        {
            this.userService = userService;
            this.travelService = travelService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var top10Drivers = this.userService.GetTop10Drivers().Select(driver => this.mapper.Map<UserHomeViewModel>(driver)); ;
            var top10Passengers = this.userService.GetTop10Passengers().Select(passenger => this.mapper.Map<UserHomeViewModel>(passenger));
            var homeModel = new HomeViewModel()
            {
                UsersCount = this.userService.GetAll().Count(),
                TravelsCount = this.travelService.GetAll().Count(),
                Top10Drivers = top10Drivers,
                Top10Passengers = top10Passengers
            };

            return View(homeModel);
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
