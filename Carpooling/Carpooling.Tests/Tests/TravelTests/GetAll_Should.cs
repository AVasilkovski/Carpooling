using Carpooling.Data;
using Carpooling.Services.DTOs;
using Carpooling.Services.Services;
using Carpooling.Services.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Carpooling.Tests.Tests.TravelTests
{
    [TestClass]
    public class GetAllTravesl_Should : BaseTest
    {
        private DbContextOptions<CarpoolingContext> options;

        [TestInitialize]
        public void Initialize()
        {
            // Arrange
            this.options = Utils.GetOptions(nameof(TestContext.TestName));

            using (var arrangeContext = new CarpoolingContext(this.options))
            {
                arrangeContext.Users.AddRange(Utils.GetUsers());
                arrangeContext.Roles.AddRange(Utils.GetRoles());
                arrangeContext.Travels.AddRange(Utils.GetTravels());
                arrangeContext.Feedbacks.AddRange(Utils.GetFeedbacks());
                arrangeContext.Cities.AddRange(Utils.GetCities());
                arrangeContext.TravelTags.AddRange(Utils.GetTravelTags());
                arrangeContext.SaveChanges();
            }
        }

        [TestMethod]
        public void ReturnAllTravels_When_ParamsAreValid()
        {
            var expectedTravels = new List<TravelPresentDTO>()
            {
                new TravelPresentDTO()
                {
                    Id = 1,
                    DepartureTime = new DateTime(2022, 2, 20, 18, 0, 0),
                    FreeSpots = 3,
                    IsCompleted = false,
                },
                new TravelPresentDTO()
                {
                    Id = 2,
                    DepartureTime = new DateTime(2022, 1, 20, 18, 0, 0),
                    FreeSpots = 2,
                    IsCompleted = true,
                }
            };
            var travelTagService = new Mock<ITravelTagService>();
            var cityService = new Mock<ICityService>();
            using (var assertContext = new CarpoolingContext(this.options))
            {
                var sut = new TravelService(assertContext, travelTagService.Object, cityService.Object);
                var actualTravels = sut.GetAll().Take(2).ToList();
                for (int i = 0; i < expectedTravels.Count; i++)
                {
                    var expected = expectedTravels[i];
                    var actual = actualTravels[i];
                    Assert.AreEqual(expected.Id, actual.Id);
                    Assert.AreEqual(expected.DepartureTime, actual.DepartureTime);
                    Assert.AreEqual(expected.FreeSpots, actual.FreeSpots);
                    Assert.AreEqual(expected.IsCompleted, actual.IsCompleted);
                }
            }
        }
    }
}
