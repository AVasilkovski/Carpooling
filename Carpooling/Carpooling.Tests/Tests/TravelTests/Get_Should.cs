using Carpooling.Data;
using Carpooling.Services.DTOs;
using Carpooling.Services.Exceptions;
using Carpooling.Services.Services;
using Carpooling.Services.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Carpooling.Tests.Tests.TravelTests
{
    [TestClass]
    public class Get_Should : BaseTest
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
        public void ReturnCorrectTravel_When_ParamsAreValid()
        {
            var id = 1;
            var travelTagService = new Mock<ITravelTagService>();
            var cityService = new Mock<ICityService>();
            var expected = new TravelPresentDTO()
            {
                Id = 1,
                DepartureTime = new DateTime(2022, 2, 20, 18, 0, 0),
                FreeSpots = 3,
                IsCompleted = false
            };

            using (var assertContext = new CarpoolingContext(this.options))
            {
                var sut = new TravelService(assertContext, travelTagService.Object, cityService.Object);
                var actual = sut.Get(id);
                Assert.AreEqual(expected.Id, actual.Id);
                Assert.AreEqual(expected.DepartureTime, actual.DepartureTime);
                Assert.AreEqual(expected.FreeSpots, actual.FreeSpots);
                Assert.AreEqual(expected.IsCompleted, actual.IsCompleted);
            }
        }

        [TestMethod]
        public void ThrowsException_When_TravelNotFound()
        {
            var travelTagService = new Mock<ITravelTagService>();
            var cityService = new Mock<ICityService>();
            using (var assertContext = new CarpoolingContext(this.options))
            {
                var sut = new TravelService(assertContext, travelTagService.Object, cityService.Object);
                Assert.ThrowsException<EntityNotFoundException>(() => sut.Get(int.MaxValue));
            }
        }
    }
}