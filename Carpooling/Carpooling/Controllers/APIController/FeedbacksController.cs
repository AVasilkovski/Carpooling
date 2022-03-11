using AutoMapper;
using Carpooling.Services.DTOs;
using Carpooling.Services.Services.Contracts;
using Carpooling.Web.Helpers.Contracts;
using Carpooling.Web.Models.APIModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Carpooling.Web.Controllers.APIController
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackService feedbackService;
        private readonly IAuthHelper authHelper;
        private readonly IMapper mapper;

        public FeedbacksController(IFeedbackService feedbackService, IAuthHelper authHelper, IMapper mapper)
        {
            this.feedbackService = feedbackService;
            this.authHelper = authHelper;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromHeader] string userName, [FromHeader] string password, int id)
        {
            try
            {
                var user = await this.authHelper.TryGetUserAsync(userName, password);
                if (user.Role.Any(role => role.Equals("User")))
                {
                    var feedback = await this.feedbackService.GetAsync(id);
                    var result = this.mapper.Map<FeedbackResponseModel>(feedback);
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
        public async Task<IActionResult> Get([FromHeader] string userName, [FromHeader] string password)
        {
            try
            {
                var user = await this.authHelper.TryGetUserAsync(userName, password);
                if (user.Role.Any(role => role.Equals("Admin")))
                {
                    var feedbacks = this.feedbackService.GetAll();
                    var result = feedbacks.Select(feedback => this.mapper.Map<FeedbackResponseModel>(feedback));
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
        public async Task<IActionResult> DeleteFeedback([FromHeader] string userName, [FromHeader] string password, int id)
        {
            try
            {
                var user = await this.authHelper.TryGetUserAsync(userName, password);
                if (user.Role.Any(role => role.Equals("Admin")))
                {
                    await this.feedbackService.DeleteAsync(id);
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
        public async Task<IActionResult> SearchRecievedFeedbacks([FromHeader] string userName, [FromHeader] string password, int userId, string username, double? rating, bool ratingSort)
        {
            try
            {
                var user = await this.authHelper.TryGetUserAsync(userName, password);
                if (user.Role.Any(role => role.Equals("User")))
                {
                    var filteredFeedbacks = this.feedbackService.SearchUserRecievedFeedbacks(userId, username, rating, ratingSort);
                    var result = filteredFeedbacks.Select(feedback => this.mapper.Map<FeedbackResponseModel>(feedback));
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
        public async Task<IActionResult> SearchGivenFeedbacks([FromHeader] string userName, [FromHeader] string password, int userId, string username, double? rating, bool ratingSort)
        {
            try
            {
                var user = await this.authHelper.TryGetUserAsync(userName, password);
                if (user.Role.Any(role => role.Equals("User")))
                {
                    var filteredFeedbacks = this.feedbackService.SearchUserGivenFeedbacks(userId, username, rating, ratingSort);
                    var result = filteredFeedbacks.Select(feedback => this.mapper.Map<FeedbackResponseModel>(feedback));
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
        public async Task<IActionResult> CreateFeedback([FromHeader] string userName, [FromHeader] string password, [FromBody] FeedbackRequestModel feedbackToBeCreated)
        {
            try
            {
                var user = await this.authHelper.TryGetUserAsync(userName, password);
                if (user.Role.Any(role => role.Equals("User") && user.Id.Equals(feedbackToBeCreated.UserFromId)))
                {
                    var feedback = this.mapper.Map<FeedbackCreateDTO>(feedbackToBeCreated);
                    var createdFeedback = await this.feedbackService.CreateAsync(feedback);
                    var result = this.mapper.Map<FeedbackResponseModel>(createdFeedback);
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
