using Carpooling.Services.Services.Contracts;
using Carpooling.Web.Helpers.Contracts;
using Carpooling.Web.Models.APIModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Carpooling.Web.Controllers.APIController
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackService feedbackService;
        private readonly IAuthHelper authHelper;

        public FeedbacksController(IFeedbackService feedbackService, IAuthHelper authHelper)
        {
            this.feedbackService = feedbackService;
            this.authHelper = authHelper;
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromHeader] string userName, [FromHeader] string password, int id)
        {
            try
            {
                var user = this.authHelper.TryGetUser(userName, password);
                if (user.Roles.Any(role => role == "User"))
                {
                    var feedback = this.feedbackService.Get(id);
                    var result = feedback.FeedbackResponseModel();
                    return Ok(result);
                }
                return BadRequest();
            }
            catch (ArgumentException e)
            {
                return this.NotFound(e);
            }
        }

        [HttpGet("")]
        public IActionResult Get([FromHeader] string userName, [FromHeader] string password)
        {
            try
            {
                var user = this.authHelper.TryGetUser(userName, password);
                if (user.Roles.Any(role => role == "Admin"))
                {
                    var feedbacks = this.feedbackService.GetAll();
                    var result = feedbacks.Select(feedback => feedback.FeedbackResponseModel());
                    return Ok(result);
                }
                return BadRequest();
            }
            catch (ArgumentException e)
            {
                return this.NotFound(e);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFeedback([FromHeader] string userName, [FromHeader] string password, int id)
        {
            try
            {
                var user = this.authHelper.TryGetUser(userName, password);
                if (user.Roles.Any(role => role == "Admin"))
                {
                    this.feedbackService.Delete(id);
                    return this.NoContent();
                }
                return BadRequest();
            }
            catch (ArgumentException e)
            {
                return this.NotFound(e);
            }
        }

        [HttpGet("Recieved/{userName}")]
        public IActionResult SearchRecievedFeedbacks([FromHeader] string userName, [FromHeader] string password, int userId, string username, double? rating, bool ratingSort)
        {
            try
            {
                var user = this.authHelper.TryGetUser(userName, password);
                if (user.Roles.Any(role => role == "User"))
                {
                    var filteredFeedbacks = this.feedbackService.SearchUserRecievedFeedbacks(userId, username, rating, ratingSort);
                    var result = filteredFeedbacks.Select(feedback => feedback.FeedbackResponseModel());
                    return this.Ok(result);
                }
                return BadRequest();
            }
            catch (ArgumentException e)
            {
                return this.NotFound(e);
            }
        }

        [HttpGet("Given/{userName}")]
        public IActionResult SearchGivenFeedbacks([FromHeader] string userName, [FromHeader] string password, int userId, string username, double? rating, bool ratingSort)
        {
            try
            {
                var user = this.authHelper.TryGetUser(userName, password);
                if (user.Roles.Any(role => role == "User"))
                {
                    var filteredFeedbacks = this.feedbackService.SearchUserGivenFeedbacks(userId, username, rating, ratingSort);
                    var result = filteredFeedbacks.Select(feedback => feedback.FeedbackResponseModel());
                    return this.Ok(result);
                }
                return BadRequest();
            }
            catch (ArgumentException e)
            {
                return this.NotFound(e);
            }
        }

        [HttpPost("")]
        public IActionResult CreateFeedback([FromHeader] string userName, [FromHeader] string password, [FromBody] FeedbackRequestModel feedbackToBeCreated)
        {
            try
            {
                var user = this.authHelper.TryGetUser(userName, password);
                if (user.Roles.Any(role => role == "User" && user.Id == feedbackToBeCreated.UserFromId))
                {
                    var feedback = feedbackToBeCreated.FeedbackRequestModel();
                    var result = this.feedbackService.Create(feedback).FeedbackResponseModel();
                    return this.Created("New Travel", result);
                }
                return this.BadRequest();
            }
            catch (ArgumentNullException e)
            {
                return this.NotFound(e);
            }
        }
    }
}
