using Carpooling.Data.Models.Enums;
using Carpooling.Services.DTOs;
using System.Collections.Generic;

namespace Carpooling.Services.Services.Contracts
{
    public interface IUserService
    {
        IEnumerable<UserPresentDTO> GetAll();

        UserPresentDTO Get(int id);

        UserPresentDTO Create(UserCreateDTO user);

        // UserCreateDTO Update(int id, UserCreateDTO updateUser);

        UserPresentDTO Update(int id, UserCreateDTO updateUser);
        UserPresentDTO Delete(int id);

        UserPresentDTO GetUserByCredentials(string username, string password);

        IEnumerable<UserPresentDTO> GetTop10Drivers();

        IEnumerable<UserPresentDTO> GetTop10Passengers();

        IEnumerable<UserPresentDTO> GetFilteredUsers(string phoneNumber, string username, string email);

        void BlockUser(int id);

        void UnblockUser(int id);

        void IsUserUnique(string username, string email, string phoneNumber);

        void UpdateUserRating(int id, FeedbackType feedbackType);
    }
}
