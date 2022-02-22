using Carpooling.Data.Models.Enums;
using Carpooling.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Carpooling.Services.Services.Contracts
{
    public interface IUserService
    {
        IEnumerable<UserPresentDTO> GetAll();

        UserPresentDTO Get(int id);

        Task<UserPresentDTO> CreateAsync(UserCreateDTO user);

        // UserCreateDTO Update(int id, UserCreateDTO updateUser);

        Task<UserPresentDTO> UpdateAsync(int id, UserCreateDTO updateUser);

        Task<UserPresentDTO> DeleteAsync(int id);

        UserPresentDTO GetUserByCredentials(string username, string password);

        IEnumerable<UserPresentDTO> GetTop10Drivers();

        IEnumerable<UserPresentDTO> GetTop10Passengers();

        IEnumerable<UserPresentDTO> GetFilteredUsers(string phoneNumber, string username, string email);

        Task BlockUserAsync(int id);

        Task UnblockUserAsync(int id);

        void IsUserUnique(string username, string email, string phoneNumber);

        Task UpdateUserRatingAsync(int id, FeedbackType feedbackType);
    }
}
