﻿using System.Collections.Generic;
using System.Linq;
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

        public UserService(CarpoolingContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private IQueryable<User> UsersQuerry
        {
            get
            {
                return this.dbContext.Users.Include(role => role.Roles)
                                           .Include(feedback => feedback.RecievedFeedbacks);
            }
        }

        public UserPresentDTO Create(UserCreateDTO userDTO)
        {
            this.IsUserUnique(userDTO.Username, userDTO.Email, userDTO.PhoneNumber);
            var user = userDTO.ToUser();
            var role = this.dbContext.Roles.FirstOrDefault(role => role.Name == "User");
            user.UserStatus = UserStatus.Active;
            user.ProfilePictureName = "defaultProfilePicture.png";
            user.Roles.Add(role);
            this.dbContext.Users.Add(user);
            this.dbContext.SaveChanges();

            return user.ToUserDTO();
        }

        public UserPresentDTO Delete(int id)
        {
            var tobeDeleted = this.GetUser(id);
            this.dbContext.Users.Remove(tobeDeleted);
            this.dbContext.SaveChanges();

            return tobeDeleted.ToUserDTO();
        }

        public UserPresentDTO Get(int id)
        {
            var user = this.GetUser(id);

            return user.ToUserDTO();
        }

        public IEnumerable<UserPresentDTO> GetAll()
        {
            var result = this.GetAllUsers();

            return result.Select(user => user.ToUserDTO());
        }

        public UserPresentDTO Update(int id, UserCreateDTO updateUser)
        {
            var user = this.GetUser(id);

            if (!string.IsNullOrEmpty(updateUser.ProfilePicture))
            {
                user.ProfilePictureName = updateUser.ProfilePicture;
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
            this.dbContext.SaveChanges();

            return user.ToUserDTO();
        }

        public UserPresentDTO GetUserByCredentials(string username, string password)
        {
            var user = this.UsersQuerry.FirstOrDefault(user => user.Username == username && user.Password == password);

            if (user == null)
            {
                throw new EntityNotFoundException();
            }

            return user.ToUserDTO();
        }

        public IEnumerable<UserPresentDTO> GetTop10Drivers()
        {
            return this.UsersQuerry.Where(user => user.RatingAsDriver > 0)
                                   .OrderByDescending(user => user.RatingAsDriver)
                                   .Take(10)
                                   .Select(user => user.ToUserDTO());
        }

        public IEnumerable<UserPresentDTO> GetTop10Passengers()
        {
            return this.UsersQuerry.Where(user => user.RatingAsPassanger > 0)
                                   .OrderByDescending(user => user.RatingAsPassanger)
                                   .Take(10)
                                   .Select(user => user.ToUserDTO());
        }

        public IEnumerable<UserPresentDTO> GetFilteredUsers(string phoneNumber, string username, string email)
        {
            return this.FilterUsers(phoneNumber, username, email).Select(user => user.ToUserDTO());
        }

        public void BlockUser(int id)
        {
            var user = this.GetUser(id);
            user.UserStatus = UserStatus.Blocked;
            this.dbContext.Users.Update(user);
            this.dbContext.SaveChanges();
        }

        public void UnblockUser(int id)
        {
            var user = this.GetUser(id);
            user.UserStatus = UserStatus.Active;
            this.dbContext.Users.Update(user);
            this.dbContext.SaveChanges();
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

        public void UpdateUserRating(int id, FeedbackType feedbackType)
        {
            var user = this.GetUser(id);

            if (feedbackType == FeedbackType.Driver)
            {
                var ratings = user.RecievedFeedbacks.Where(feedbacks => feedbacks.Type == FeedbackType.Driver).Select(feedback => feedback.Rating);
                var average = ratings.Average();
                user.RatingAsPassanger = average;
            }
            else
            {
                var ratings = user.RecievedFeedbacks.Where(feedbacks => feedbacks.Type == FeedbackType.Passenger).Select(feedback => feedback.Rating);
                var average = ratings.Average();
                user.RatingAsDriver = average;
            }

            this.dbContext.Update(user);
            this.dbContext.SaveChanges();
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
            return this.UsersQuerry.Where(user => user.Roles.Any(role => role.Name != "Admin"));
        }

        private User GetUser(int id)
        {
            var user = this.UsersQuerry.FirstOrDefault(user => user.Id == id);

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