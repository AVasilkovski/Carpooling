using Carpooling.Data;
using Carpooling.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Carpooling.Tests
{
    public class Utils
    {
        public static DbContextOptions<CarpoolingContext> GetOptions(string databaseName)
        {
            return new DbContextOptionsBuilder<CarpoolingContext>()
                       .UseInMemoryDatabase(databaseName)
                       .Options;
        }

        public static IEnumerable<User> GetUsers()
        {
            return ModelBuilderExtensions.Users;
        }

        public static IEnumerable<City> GetCities()
        {
            return ModelBuilderExtensions.Cities;
        }

        public static IEnumerable<Feedback> GetFeedbacks()
        {
            return ModelBuilderExtensions.Feedbacks;
        }

        public static IEnumerable<Role> GetRoles()
        {
            return ModelBuilderExtensions.Roles;
        }

        public static IEnumerable<Travel> GetTravels()
        {
            return ModelBuilderExtensions.Travels;
        }

        public static IEnumerable<TravelTag> GetTravelTags()
        {
            return ModelBuilderExtensions.TravelTags;
        }
    }
}
