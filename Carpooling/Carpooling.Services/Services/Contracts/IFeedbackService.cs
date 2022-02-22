using Carpooling.Services.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carpooling.Services.Services.Contracts
{
    public interface IFeedbackService
    {
        IQueryable<FeedbackPresentDTO> GetAll();

        FeedbackPresentDTO Get(int id);

        Task<FeedbackPresentDTO> CreateAsync(FeedbackCreateDTO feedbackCreateDTO);

        Task<FeedbackPresentDTO> DeleteAsync(int id);

        IEnumerable<FeedbackPresentDTO> SearchUserRecievedFeedbacks(int userId, string username, double? rating, bool ratingSort);

        IEnumerable<FeedbackPresentDTO> SearchUserGivenFeedbacks(int userId, string username, double? rating, bool ratingSort);
    }
}