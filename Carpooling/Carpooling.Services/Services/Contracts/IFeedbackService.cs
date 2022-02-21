using Carpooling.Services.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace Carpooling.Services.Services.Contracts
{
    public interface IFeedbackService
    {
        IQueryable<FeedbackPresentDTO> GetAll();

        FeedbackPresentDTO Get(int id);

        FeedbackPresentDTO Create(FeedbackCreateDTO feedbackCreateDTO);

        FeedbackPresentDTO Delete(int id);

        IEnumerable<FeedbackPresentDTO> SearchUserRecievedFeedbacks(int userId, string username, double? rating, bool ratingSort);

        IEnumerable<FeedbackPresentDTO> SearchUserGivenFeedbacks(int userId, string username, double? rating, bool ratingSort);
    }
}