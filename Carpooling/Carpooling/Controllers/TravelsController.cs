using Carpooling.Services.Services.Contracts;
using Carpooling.Web.Helpers;
using Carpooling.Web.Helpers.Contracts;
using Carpooling.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Carpooling.Web.Controllers
{
    [AuthorizationAttribute(Role = "User")]
    public class TravelsController : Controller
    {
        private readonly ITravelService travelService;
        private readonly ITravelTagHelper travelTagHelper;

        public TravelsController(ITravelService travelService, ITravelTagHelper travelTagHelper)
        {
            this.travelService = travelService;
            this.travelTagHelper = travelTagHelper;
        }

        public IActionResult Index(string startCity, string destinationCity, string driver, int? spots, bool dateSort = false, bool spotsSort = false)
        {
            ViewData["StartCity"] = startCity;
            ViewData["DestinationCity"] = destinationCity;
            ViewData["Driver"] = driver;
            ViewData["Spots"] = spots;

            var user = this.HttpContext.Session.GetString("CurrentUser");
            var travels = this.travelService.SearchAvailableTravels(user, startCity, destinationCity, driver, spots, dateSort, spotsSort);
            var searchResult = travels.Select(travel => travel.ToTravelViewModel());
            return View(searchResult);
        }

        public IActionResult Create()
        {
            var travelCreateViewModel = new TravelCreateViewModel();
            travelCreateViewModel.Tags = this.travelTagHelper.ListTags();
            return View(travelCreateViewModel);
        }

        [HttpPost]
        public IActionResult Create(TravelCreateViewModel travelCreateViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                travelCreateViewModel.Tags = this.travelTagHelper.ListTags();
                return this.View(travelCreateViewModel);
            }
            if (travelCreateViewModel.SlectedTags == null)
            {
                this.ModelState.AddModelError("TagError", "Please select a tag");
                travelCreateViewModel.Tags = this.travelTagHelper.ListTags();
                return this.View(travelCreateViewModel);
            }

            var driverId = int.Parse(this.HttpContext.Session.GetString("UserId"));
            var travel = travelCreateViewModel.ToTravelCreateDTO(driverId);
            this.travelService.Create(travel);
            return RedirectToAction("Index", "MyTravels");
        }

        public IActionResult Update(int id)
        {
            var travelUpdateViewModel = new TravelUpdateViewModel();
            return View(travelUpdateViewModel);
        }

        [HttpPost]
        public IActionResult Update(int id, TravelUpdateViewModel travelUpdateViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(travelUpdateViewModel);
            }

            var travel = travelUpdateViewModel.ToTravelUpdate();
            this.travelService.Update(id, travel);
            return RedirectToAction("Index", "MyTravels");
        }

        public IActionResult Details(int id)
        {
            var travel = this.travelService.Get(id);
            var travelDetails = travel.ToTravelDetailsViewModel();
            return View(travelDetails);
        }

        public IActionResult Apply(int id)
        {
            var userId = int.Parse(this.HttpContext.Session.GetString("UserId"));
            this.travelService.ApplyAsPassenger(userId, id);
            return RedirectToAction("Index", "Travels");
        }

        public IActionResult Complete(int id)
        {
            this.travelService.MarkAsComplete(id);
            return RedirectToAction("Index", "MyTravels");
        }

        public IActionResult Cancel(int id)
        {
            this.travelService.CancelTrip(id);
            return RedirectToAction("Index", "MyTravels");
        }

        public IActionResult Accept(int id, int travelId)
        {
            var driverId = int.Parse(this.HttpContext.Session.GetString("UserId"));
            this.travelService.AddPassenger(id, driverId, travelId);
            return RedirectToAction("Details", "Travels", new { Id = travelId });
        }

        public IActionResult Reject(int id, int travelId)
        {
            var driverId = int.Parse(this.HttpContext.Session.GetString("UserId"));
            this.travelService.RejectPassenger(id, driverId, travelId);
            return RedirectToAction("Details", "Travels", new { Id = travelId });
        }

        public IActionResult Leave(int id)
        {
            var userId = int.Parse(this.HttpContext.Session.GetString("UserId"));
            this.travelService.CancelParticipation(userId, id);
            return RedirectToAction("Index", "Travels");
        }
    }
}