using Carpooling.Services.Services.Contracts;
using Carpooling.Web.Helpers;
using Carpooling.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Carpooling.Web.Controllers
{
    [AuthorizationAttribute(Role = "User")]
    public class FeedbacksController : Controller
    {
        private readonly IFeedbackService feedbackService;
        private readonly ITravelService travelService;

        public FeedbacksController(IFeedbackService feedbackService, ITravelService travelService)
        {
            this.feedbackService = feedbackService;
            this.travelService = travelService;
        }

        public IActionResult Received(int id, string username, double? rating, bool ratingSort)
        {
            ViewData["VisitedFeedbacksUserId"] = id;

            var feedbacks = this.feedbackService.SearchUserRecievedFeedbacks(id, username, rating, ratingSort);
            var searchResult = feedbacks.Select(feedback => feedback.ToFeedbackReceivedViewModel());
            return View(searchResult);
        }

        public IActionResult Given(int id, string username, double? rating, bool ratingSort)
        {
            ViewData["VisitedFeedbacksUserId"] = id;

            var feedbacks = this.feedbackService.SearchUserGivenFeedbacks(id, username, rating, ratingSort);
            var searchResult = feedbacks.Select(feedback => feedback.ToFeedbackGivenViewModel());
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
        public IActionResult Create(FeedbackCreateViewModel feedbackCreateViewModel, int userToId, int travelId)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(feedbackCreateViewModel);
            }

            var userFromId = int.Parse(this.HttpContext.Session.GetString("UserId"));
            var travel = this.travelService.Get(travelId);
            var feedback = feedbackCreateViewModel.ToFeedbackCreateDTO();
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

            this.feedbackService.Create(feedback);
            return RedirectToAction("Participants", "MyTravels", new { id = travelId });
        }
    }
}
