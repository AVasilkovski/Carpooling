using Carpooling.Services.Services.Contracts;
using Carpooling.Web.Helpers;
using Carpooling.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Carpooling.Web.Controllers
{
    [AuthorizationAttribute(Role = "User")]
    public class MyTravelsController : Controller
    {
        private readonly ITravelService travelService;

        public MyTravelsController(ITravelService travelService)
        {
            this.travelService = travelService;
        }

        public IActionResult Index(string startCity, string destinationCity, int? spots, bool dateSort = false, bool spotsSort = false)
        {
            ViewData["StartCity"] = startCity;
            ViewData["DestinationCity"] = destinationCity;
            ViewData["Spots"] = spots;

            var user = this.HttpContext.Session.GetString("CurrentUser");
            var travels = this.travelService.SearchUserAsDriverTravels(user, startCity, destinationCity, spots, dateSort, spotsSort);
            var searchResult = travels.Select(travel => travel.ToTravelViewModel());
            return View(searchResult);
        }

        public IActionResult Finished(string startCity, string destinationCity, string driver, int? spots, bool dateSort = false, bool spotsSort = false)
        {
            ViewData["StartCity"] = startCity;
            ViewData["DestinationCity"] = destinationCity;
            ViewData["Driver"] = driver;
            ViewData["Spots"] = spots;

            var user = this.HttpContext.Session.GetString("CurrentUser");
            var travels = this.travelService.SearchFinishedUserTravels(user, startCity, destinationCity, driver, spots, dateSort, spotsSort);
            var searchResult = travels.Select(travel => travel.ToTravelViewModel());
            return View(searchResult);
        }

        public IActionResult Applied(string startCity, string destinationCity, string driver, int? spots, bool dateSort = false, bool spotsSort = false)
        {
            ViewData["StartCity"] = startCity;
            ViewData["DestinationCity"] = destinationCity;
            ViewData["Driver"] = driver;
            ViewData["Spots"] = spots;

            var user = this.HttpContext.Session.GetString("CurrentUser");
            var travels = this.travelService.SearchAppliedUserTravels(user, startCity, destinationCity, driver, spots, dateSort, spotsSort);
            var searchResult = travels.Select(travel => travel.ToTravelViewModel());
            return View(searchResult);
        }

        public IActionResult Approved(string startCity, string destinationCity, string driver, int? spots, bool dateSort = false, bool spotsSort = false)
        {
            ViewData["StartCity"] = startCity;
            ViewData["DestinationCity"] = destinationCity;
            ViewData["Driver"] = driver;
            ViewData["Spots"] = spots;

            var user = this.HttpContext.Session.GetString("CurrentUser");
            var travels = this.travelService.SearchApprovedUserTravels(user, startCity, destinationCity, driver, spots, dateSort, spotsSort);
            var searchResult = travels.Select(travel => travel.ToTravelViewModel());
            return View(searchResult);
        }


        public IActionResult Participants(int id)
        {
            ViewData["TravelId"] = id;

            var travel = this.travelService.Get(id);
            var driver = travel.Driver.ToUserProfileViewModel();
            var passengers = travel.Passengers.Select(passenger => passenger.ToUserProfileViewModel());
            var feedbacks = travel.Feedbacks.Select(feedback => feedback.ToFeedbackViewModel());
            var participants = driver.ToParticipantsViewModel(passengers, feedbacks);
            return View(participants);
        }
    }
}
