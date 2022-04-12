using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Carpooling.Data;
using Carpooling.Data.Models;
using Carpooling.Services.DTOs;
using Carpooling.Services.Services.Contracts;
using Carpooling.Services.Exceptions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Carpooling.Services.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly CarpoolingContext dbContext;
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public FeedbackService(CarpoolingContext dbContext, IUserService userService, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.userService = userService;
            this.mapper = mapper;
        }

        private IQueryable<Feedback> FeedbacksQuery
        {
            get
            {
                return this.dbContext.Feedbacks.Include(user => user.UserFrom)
                                                    .ThenInclude(role => role.Role)
                                               .Include(user => user.UserTo)
                                                    .ThenInclude(role => role.Role);
            }
        }

        public IQueryable<FeedbackPresentDTO> GetAll()
        {
            return this.dbContext.Feedbacks.ProjectTo<FeedbackPresentDTO>(mapper.ConfigurationProvider);
        }

        public async Task<FeedbackPresentDTO> GetAsync(int id)
        {
            var feedback = await this.GetFeedbackAsync(id);

            return mapper.Map<FeedbackPresentDTO>(feedback);
        }

        public async Task<FeedbackPresentDTO> CreateAsync(FeedbackCreateDTO feedbackCreateDTO)
        {
            await this.userService.UpdateUserRatingAsync(feedbackCreateDTO.UserToId, feedbackCreateDTO.Type);
            var feedback = mapper.Map<Feedback>(feedbackCreateDTO);
            await this.dbContext.Feedbacks.AddAsync(feedback);
            await this.dbContext.SaveChangesAsync();
            feedback = await this.GetFeedbackAsync(feedback.Id);

            return mapper.Map<FeedbackPresentDTO>(feedback);
        }

        public async Task<FeedbackPresentDTO> DeleteAsync(int id)
        {
            var feedback = await this .GetFeedbackAsync(id);
            this.dbContext.Feedbacks.Remove(feedback);
            await this.dbContext.SaveChangesAsync();

            return mapper.Map<FeedbackPresentDTO>(feedback);
        }

        public IEnumerable<FeedbackPresentDTO> SearchUserGivenFeedbacks(int userId, string username, double? rating, bool ratingSort)
        {
            var userGivenFeedbacks = this.dbContext.Feedbacks.Where(feedback => feedback.UserFromId == userId)
                .ProjectTo<FeedbackPresentDTO>(mapper.ConfigurationProvider).ToList();
            var serachResult = this.SearchFeedbacks(userGivenFeedbacks, username, rating, ratingSort);

            return serachResult;
        }

        public IEnumerable<FeedbackPresentDTO> SearchUserRecievedFeedbacks(int userId, string username, double? rating, bool ratingSort)
        {
            var userRecievedFeedbacks = this.dbContext.Feedbacks.Where(feedback => feedback.UserToId == userId)
                .ProjectTo<FeedbackPresentDTO>(mapper.ConfigurationProvider).ToList();
            var searchResult = this.SearchFeedbacks(userRecievedFeedbacks, username, rating, ratingSort);

            return searchResult;
        }

        private IEnumerable<FeedbackPresentDTO> SearchFeedbacks(IEnumerable<FeedbackPresentDTO> feedbacks, string username, double? rating, bool ratingSort)
        {
            if (username != null)
            {
                feedbacks = feedbacks.Where(feedback => feedback.UserFromUsername.Contains(username));
            }

            if (rating != null)
            {
                feedbacks = feedbacks.Where(feedback => feedback.Rating == rating);
            }

            if (ratingSort)
            {
                feedbacks = feedbacks.OrderByDescending(feedback => feedback.Rating);
            }

            return feedbacks;
        }

        private async Task<Feedback> GetFeedbackAsync(int id)
        {
            var feedback = await this.FeedbacksQuery.FirstOrDefaultAsync(feedback => feedback.Id == id);

            if (feedback == null)
            {
                throw new EntityNotFoundException("Feedback not found");
            }

            return feedback;
        }
    }
}