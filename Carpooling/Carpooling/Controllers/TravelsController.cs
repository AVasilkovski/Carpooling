using AutoMapper;
using Carpooling.Services.DTOs;
using Carpooling.Services.Services.Contracts;
using Carpooling.Web.Helpers;
using Carpooling.Web.Helpers.Contracts;
using Carpooling.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Carpooling.Web.Controllers
{
    [AuthorizationAttribute(Role = "User")]
    public class TravelsController : Controller
    {
        private readonly ITravelService travelService;
        private readonly ITravelTagHelper travelTagHelper;
        private readonly IMapper mapper;

        public TravelsController(ITravelService travelService, ITravelTagHelper travelTagHelper, IMapper mapper)
        {
            this.travelService = travelService;
            this.travelTagHelper = travelTagHelper;
            this.mapper = mapper;
        }

        public IActionResult Index(string startCity, string destinationCity, string driver, int? spots, bool dateSort = false, bool spotsSort = false)
        {
            ViewData["StartCity"] = startCity;
            ViewData["DestinationCity"] = destinationCity;
            ViewData["Driver"] = driver;
            ViewData["Spots"] = spots;

            var user = this.HttpContext.Session.GetString("CurrentUser");
            var travels = this.travelService.SearchAvailableTravels(user, startCity, destinationCity, driver, spots, dateSort, spotsSort);
            var searchResult = travels.Select(travel => this.mapper.Map<TravelViewModel>(travel));
            return View(searchResult);
        }

        public IActionResult Create()
        {
            var travelCreateViewModel = new TravelCreateViewModel();
            travelCreateViewModel.Tags = this.travelTagHelper.ListTags();
            return View(travelCreateViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TravelCreateViewModel travelCreateViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                travelCreateViewModel.Tags = this.travelTagHelper.ListTags();
                return this.View(travelCreateViewModel);
            }
            if (travelCreateViewModel.Tags == null)
            {
                this.ModelState.AddModelError("TagError", "Please select a tag");
                travelCreateViewModel.Tags = this.travelTagHelper.ListTags();
                return this.View(travelCreateViewModel);
            }

            var driverId = int.Parse(this.HttpContext.Session.GetString("UserId"));
            travelCreateViewModel.DriverID = driverId;
            var travel = this.mapper.Map<TravelCreateDTO>(travelCreateViewModel);
            await this.travelService.CreateAsync(travel);
            return RedirectToAction("Index", "MyTravels");
        }

        public IActionResult Update(int id)
        {
            var travelUpdateViewModel = new TravelUpdateViewModel();
            return View(travelUpdateViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, TravelUpdateViewModel travelUpdateViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(travelUpdateViewModel);
            }

            var travel = this.mapper.Map<TravelCreateDTO>(travelUpdateViewModel); 
            await this.travelService.UpdateAsync(id, travel);
            return RedirectToAction("Index", "MyTravels");
        }

        public async Task<IActionResult> Details(int id)
        {
            var travel = await this.travelService.GetAsync(id);

            var travelDetails = this.mapper.Map<TravelDetailsViewModel>(travel);
            return View(travelDetails);
        }

        public async Task<IActionResult> Apply(int id)
        {
            var userId = int.Parse(this.HttpContext.Session.GetString("UserId"));
            await this.travelService.ApplyAsPassengerAsync(userId, id);
            return RedirectToAction("Index", "Travels");
        }

        public async Task<IActionResult> Complete(int id)
        {
            await this.travelService.MarkAsCompleteAsync(id);
            return RedirectToAction("Index", "MyTravels");
        }

        public async Task<IActionResult> Cancel(int id)
        {
            await this.travelService.CancelTripAsync(id);
            return RedirectToAction("Index", "MyTravels");
        }

        public async Task<IActionResult> Accept(int id, int travelId)
        {
            var driverId = int.Parse(this.HttpContext.Session.GetString("UserId"));
            await this.travelService.AddPassengerAsync(id, driverId, travelId);
            return RedirectToAction("Details", "Travels", new { Id = travelId });
        }

        public async Task<IActionResult> Reject(int id, int travelId)
        {
            var driverId = int.Parse(this.HttpContext.Session.GetString("UserId"));
            await this.travelService.RejectPassengerAsync(id, driverId, travelId);
            return RedirectToAction("Details", "Travels", new { Id = travelId });
        }

        public async Task<IActionResult> Leave(int id)
        {
            var userId = int.Parse(this.HttpContext.Session.GetString("UserId"));
            await this.travelService.CancelParticipationAsync(userId, id);
            return RedirectToAction("Index", "Travels");
        }
    }
}