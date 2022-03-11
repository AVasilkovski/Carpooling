using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Carpooling.Data;
using Carpooling.Data.Models;
using Carpooling.Data.Models.Enums;
using Carpooling.Services.DTOs;
using Carpooling.Services.Exceptions;
using Carpooling.Services.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Carpooling.Services.Services
{
    public class UserService : IUserService
    {
        private readonly CarpoolingContext dbContext;
        private readonly IMapper mapper;

        public UserService(CarpoolingContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        private IQueryable<User> UsersQuerry
        {
            get
            {
                return this.dbContext.Users.Include(role => role.Role)
                                           .Include(feedback => feedback.RecievedFeedbacks);
            }
        }

        public async Task<UserPresentDTO> CreateAsync(UserCreateDTO userDTO)
        {
            this.IsUserUnique(userDTO.Username, userDTO.Email, userDTO.PhoneNumber);
            var user = mapper.Map<User>(userDTO);
            var role = await this.dbContext.Roles.FirstOrDefaultAsync(role => role.Name == "User");
            user.UserStatus = UserStatus.Active;
            user.ProfilePictureName = "defaultProfilePicture.png";
            user.Role.Add(role);
            await this.dbContext.Users.AddAsync(user);  
            await this.dbContext.SaveChangesAsync();

            return this.mapper.Map<UserPresentDTO>(user);
        }

        public async Task<UserPresentDTO> DeleteAsync(int id)
        {
            var tobeDeleted = await this.GetUserAsync(id);
            this.dbContext.Users.Remove(tobeDeleted);
            await this.dbContext.SaveChangesAsync();

            return this.mapper.Map<UserPresentDTO>(tobeDeleted);
        }

        public async Task<UserPresentDTO> GetAsync(int id)
        {
            var user = await this.GetUserAsync(id);

            return this.mapper.Map<UserPresentDTO>(user);
        }
        public UserPresentDTO Get(int id)
        {
            var user =  this.GetUserAsync(id);

            return this.mapper.Map<UserPresentDTO>(user);
        }

        public IEnumerable<UserPresentDTO> GetAll()
        {
            var result = this.GetAllUsers();

            return result.Select(user => mapper.Map<UserPresentDTO>(user));
        }

        public async Task<UserPresentDTO> UpdateAsync(int id, UserCreateDTO updateUser)
        {
            var user = await this.GetUserAsync(id);

            if (!string.IsNullOrEmpty(updateUser.ProfilePictureName))
            {
                user.ProfilePictureName = updateUser.ProfilePictureName;
            }

            if (!string.IsNullOrEmpty(updateUser.Password))
            {
                user.Password = updateUser.Password;
            }

            if (!string.IsNullOrEmpty(updateUser.FirstName))
            {
                user.FirstName = updateUser.FirstName;
            }

            if (!string.IsNullOrEmpty(updateUser.LastName))
            {
                user.LastName = updateUser.LastName;
            }

            if (!string.IsNullOrEmpty(updateUser.Email))
            {
                user.Email = updateUser.Email;
            }

            if (!string.IsNullOrEmpty(updateUser.PhoneNumber))
            {
                user.PhoneNumber = updateUser.PhoneNumber;
            }

            this.dbContext.Users.Update(user);
            await this.dbContext.SaveChangesAsync();

            return this.mapper.Map<UserPresentDTO>(user);
        }

        public async Task<UserPresentDTO> GetUserByCredentialsAsync(string username, string password)
        {
            var user = await this.UsersQuerry.FirstOrDefaultAsync(user => user.Username == username && user.Password == password);

            if (user == null)
            {
                throw new EntityNotFoundException();
            }

            return this.mapper.Map<UserPresentDTO>(user);
        }

        public IEnumerable<UserPresentDTO> GetTop10Drivers()
        {
            return this.UsersQuerry.Where(user => user.RatingAsDriver > 0)
                                   .OrderByDescending(user => user.RatingAsDriver)
                                   .Take(10)
                                   .Select(user => mapper.Map<UserPresentDTO>(user));
        }

        public IEnumerable<UserPresentDTO> GetTop10Passengers()
        {
            return this.UsersQuerry.Where(user => user.RatingAsPassenger > 0)
                                   .OrderByDescending(user => user.RatingAsPassenger)
                                   .Take(10)
                                   .Select(user => mapper.Map<UserPresentDTO>(user));
        }

        public IEnumerable<UserPresentDTO> GetFilteredUsers(string phoneNumber, string username, string email)
        {
            return this.FilterUsers(phoneNumber, username, email).Select(user => mapper.Map<UserPresentDTO>(user));
        }

        public async Task BlockUserAsync(int id)
        {
            var user = await this.GetUserAsync(id);
            user.UserStatus = UserStatus.Blocked;
            this.dbContext.Users.Update(user);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task UnblockUserAsync(int id)
        {
            var user = await this.GetUserAsync(id);
            user.UserStatus = UserStatus.Active;
            this.dbContext.Users.Update(user);
            await this.dbContext.SaveChangesAsync();
        }

        public void IsUserUnique(string username, string email, string phoneNumber)
        {
            if (this.UsersQuerry.Any(user => user.Username == username))
            {
                throw new EntityAlreadyExistsException("Username is already in registered.");
            }

            if (this.UsersQuerry.Any(user => user.Email == email))
            {
                throw new EntityAlreadyExistsException("Email is alredy registered.");
            }

            if (this.UsersQuerry.Any(user => user.PhoneNumber == phoneNumber))
            {
                throw new EntityAlreadyExistsException("Phone number is already registered");
            }
        }

        public async Task UpdateUserRatingAsync(int id, FeedbackType feedbackType)
        {
            var user = await this.GetUserAsync(id);

            if (feedbackType == FeedbackType.Driver)
            {
                var ratings = user.RecievedFeedbacks.Where(feedbacks => feedbacks.Type == FeedbackType.Driver).Select(feedback => feedback.Rating);
                var average = ratings.Average();
                user.RatingAsPassenger = average;
            }
            else
            {
                var ratings = user.RecievedFeedbacks.Where(feedbacks => feedbacks.Type == FeedbackType.Passenger).Select(feedback => feedback.Rating);
                var average = ratings.Average();
                user.RatingAsDriver = average;
            }

            this.dbContext.Update(user);
            await this.dbContext.SaveChangesAsync();
        }

        private IEnumerable<User> FilterUsers(string phoneNumber, string username, string email)
        {
            var users = this.GetAllUsers();

            if (phoneNumber != null)
            {
                users = users.Where(user => user.PhoneNumber.Contains(phoneNumber));
            }

            if (username != null)
            {
                users = users.Where(user => user.Username.Contains(username));
            }

            if (email != null)
            {
                users = users.Where(user => user.Email.Contains(email));
            }

            return users;
        }

        private IEnumerable<User> GetAllUsers()
        {
            return this.UsersQuerry.Where(user => user.Role.Any(role => role.Name != "Admin"));
        }

        private async Task<User> GetUserAsync(int id)
        {
            var user = await this.UsersQuerry.FirstOrDefaultAsync(user => user.Id == id);

            if (user != null)
            {
                return user;
            }
            else
            {
                throw new EntityNotFoundException($"User with id: {id} was not found.");
            }
        }
    }
}