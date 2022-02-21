using Carpooling.Data;
using Carpooling.Services.Exceptions;
using Carpooling.Services.Services;
using Carpooling.Services.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Carpooling.Tests.Tests.TravelTests
{
    [TestClass]
    public class RemovePassenger_Should : BaseTest
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
        public void Throws_When_PassengerNotFound()
        {
            var userId = 10;
            var travelId = 3;
            var driverId = 3;
            var travelTagService = new Mock<ITravelTagService>();
            var cityService = new Mock<ICityService>();
            using (var assertContext = new CarpoolingContext(this.options))
            {
                var sut = new TravelService(assertContext, travelTagService.Object, cityService.Object);
                Assert.ThrowsException<EntityNotFoundException>(() => sut.RejectPassenger(userId, driverId, travelId));
            }
        }

        [TestMethod]
        public void RemovePassenger_When_ParamsAreValid()
        {
            var userId = 10;
            var travelId = 3;
            var driverId = 3;
            var travelTagService = new Mock<ITravelTagService>();
            var cityService = new Mock<ICityService>();
            using (var assertContext = new CarpoolingContext(this.options))
            {
                var expected = 4;
                var sut = new TravelService(assertContext, travelTagService.Object, cityService.Object);
                sut.ApplyAsPassenger(userId, travelId);
                sut.AddPassenger(userId, driverId, travelId);
                sut.RejectPassenger(userId, driverId, travelId);
                var actual = sut.Get(travelId).FreeSpots;
                Assert.AreEqual(expected, actual);
            }
        }
    }
}
