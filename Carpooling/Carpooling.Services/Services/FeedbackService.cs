using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Carpooling.Data;
using Carpooling.Data.Models;
using Carpooling.Services.DTOs;
using Carpooling.Services.Services.Contracts;
using Carpooling.Services.Exceptions;

namespace Carpooling.Services.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly CarpoolingContext dbContext;
        private readonly IUserService userService;

        public FeedbackService(CarpoolingContext dbContext, IUserService userService)
        {
            this.dbContext = dbContext;
            this.userService = userService;
        }

        private IQueryable<Feedback> FeedbacksQuery
        {
            get
            {
                return this.dbContext.Feedbacks.Include(user => user.UserFrom)
                                                    .ThenInclude(role => role.Roles)
                                               .Include(user => user.UserTo)
                                                    .ThenInclude(role => role.Roles);
            }
        }

        public IQueryable<FeedbackPresentDTO> GetAll()
        {
            return this.FeedbacksQuery.Select(feedback => feedback.ToFeedbackDTO());
        }

        public FeedbackPresentDTO Get(int id)
        {
            var feedback = this.GetFeedback(id);

            return feedback.ToFeedbackDTO();
        }

        public FeedbackPresentDTO Create(FeedbackCreateDTO feedbackCreateDTO)
        {
            this.userService.UpdateUserRating(feedbackCreateDTO.UserToId, feedbackCreateDTO.Type);
            var feedback = feedbackCreateDTO.ToFeedback();
            this.dbContext.Feedbacks.Add(feedback);
            this.dbContext.SaveChanges();
            feedback = this.GetFeedback(feedback.Id);

            return feedback.ToFeedbackDTO();
        }

        public FeedbackPresentDTO Delete(int id)
        {
            var feedback = this.GetFeedback(id);
            this.dbContext.Feedbacks.Remove(feedback);
            this.dbContext.SaveChanges();

            return feedback.ToFeedbackDTO();
        }

        public IEnumerable<FeedbackPresentDTO> SearchUserGivenFeedbacks(int userId, string username, double? rating, bool ratingSort)
        {
            var userGivenFeedbacks = this.FeedbacksQuery.Where(feedback => feedback.UserFromId == userId);
            var serachResult = this.SearchFeedbacks(userGivenFeedbacks, username, rating, ratingSort);

            return serachResult;
        }

        public IEnumerable<FeedbackPresentDTO> SearchUserRecievedFeedbacks(int userId, string username, double? rating, bool ratingSort)
        {
            var userRecievedFeedbacks = this.FeedbacksQuery.Where(feedback => feedback.UserToId == userId);
            var searchResult = this.SearchFeedbacks(userRecievedFeedbacks, username, rating, ratingSort);

            return searchResult;
        }

        private IEnumerable<FeedbackPresentDTO> SearchFeedbacks(IQueryable<Feedback> feedbacks, string username, double? rating, bool ratingSort)
        {
            if (username != null)
            {
                feedbacks = feedbacks.Where(feedback => feedback.UserFrom.Username.Contains(username));
            }

            if (rating != null)
            {
                feedbacks = feedbacks.Where(feedback => feedback.Rating == rating);
            }

            if (ratingSort)
            {
                feedbacks = feedbacks.OrderByDescending(feedback => feedback.Rating);
            }

            var result = feedbacks.Select(feedback => feedback.ToFeedbackDTO());

            return result;
        }

        private Feedback GetFeedback(int id)
        {
            var feedback = this.FeedbacksQuery.FirstOrDefault(feedback => feedback.Id == id);

            if (feedback == null)
            {
                throw new EntityNotFoundException("Feedback not found");
            }

            return feedback;
        }
    }
}