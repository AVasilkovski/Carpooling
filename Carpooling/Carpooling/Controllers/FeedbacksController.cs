using AutoMapper;
using Carpooling.Services.DTOs;
using Carpooling.Services.Services.Contracts;
using Carpooling.Web.Helpers;
using Carpooling.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Carpooling.Web.Controllers
{
    [AuthorizationAttribute(Role = "User")]
    public class FeedbacksController : Controller
    {
        private readonly IFeedbackService feedbackService;
        private readonly ITravelService travelService;
        private readonly IMapper mapper;

        public FeedbacksController(IFeedbackService feedbackService, ITravelService travelService, IMapper mapper)
        {
            this.feedbackService = feedbackService;
            this.travelService = travelService;
            this.mapper = mapper;
        }

        public IActionResult Received(int id, string username, double? rating, bool ratingSort)
        {
            ViewData["VisitedFeedbacksUserId"] = id;

            var feedbacks = this.feedbackService.SearchUserRecievedFeedbacks(id, username, rating, ratingSort);
            var searchResult = feedbacks.Select(feedback => this.mapper.Map<FeedbackSearchViewModel>(feedback));
            return View(searchResult);
        }

        public IActionResult Given(int id, string username, double? rating, bool ratingSort)
        {
            ViewData["VisitedFeedbacksUserId"] = id;

            var feedbacks = this.feedbackService.SearchUserGivenFeedbacks(id, username, rating, ratingSort);
            var searchResult = feedbacks.Select(feedback => this.mapper.Map<FeedbackSearchViewModel>(feedback));
            return View(searchResult);
        }

        public IActionResult Create(int id, int travelId)
        {
            ViewData["Travel"] = travelId;
            ViewData["UserTo"] = id;
            var feedbackCreate = new FeedbackCreateViewModel();
            return View(feedbackCreate);
        }

        [HttpPost]
        public async Task<IActionResult> Create(FeedbackCreateViewModel feedbackCreateViewModel, int userToId, int travelId)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(feedbackCreateViewModel);
            }

            var userFromId = int.Parse(this.HttpContext.Session.GetString("UserId"));
            var travel = await this.travelService.GetAsync(travelId);
            var feedback = this.mapper.Map<FeedbackCreateDTO>(feedbackCreateViewModel);
            feedback.UserFromId = userFromId;
            feedback.TravelId = travelId;
            feedback.UserToId = userToId;
            if (travel.Driver.Id == userFromId)
            {
                feedback.Type = Data.Models.Enums.FeedbackType.Driver;
            }
            else
            {
                feedback.Type = Data.Models.Enums.FeedbackType.Passenger;
            }

            await this.feedbackService.CreateAsync(feedback);
            return RedirectToAction("Participants", "MyTravels", new { id = travelId });
        }
    }
}
