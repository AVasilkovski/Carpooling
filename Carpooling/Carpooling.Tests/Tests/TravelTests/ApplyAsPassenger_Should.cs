using Carpooling.Data;
using Carpooling.Services.Exceptions;
using Carpooling.Services.Services;
using Carpooling.Services.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace Carpooling.Tests.Tests.TravelTests
{
    [TestClass]
    public class ApplyAsPassenger_Should : BaseTest
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
        public void Throw_When_DriverApplies()
        {
            var userId = 1;
            var travelId = 1;
            var travelTagService = new Mock<ITravelTagService>();
            var cityService = new Mock<ICityService>();
            using (var assertContext = new CarpoolingContext(this.options))
            {

                var sut = new TravelService(assertContext, travelTagService.Object, cityService.Object);
                Assert.ThrowsException<TravelException>(() => sut.ApplyAsPassenger(userId, travelId));
            }
        }

        [TestMethod]
        public void Throw_When_UserNotFound()
        {
            var userId = int.MaxValue;
            var travelId = 1;
            var travelTagService = new Mock<ITravelTagService>();
            var cityService = new Mock<ICityService>();
            using (var assertContext = new CarpoolingContext(this.options))
            {

                var sut = new TravelService(assertContext, travelTagService.Object, cityService.Object);
                Assert.ThrowsException<EntityNotFoundException>(() => sut.ApplyAsPassenger(userId, travelId));
            }
        }

        [TestMethod]
        public void Throw_When_TravelIsCompleted()
        {
            var userId = 5;
            var travelId = 2;
            var travelTagService = new Mock<ITravelTagService>();
            var cityService = new Mock<ICityService>();
            using (var assertContext = new CarpoolingContext(this.options))
            {

                var sut = new TravelService(assertContext, travelTagService.Object, cityService.Object);
                Assert.ThrowsException<TravelException>(() => sut.ApplyAsPassenger(userId, travelId));
            }
        }

        [TestMethod]
        public void ApplyUser_When_ParamsAreValid()
        {
            var userId = 6;
            var travelId = 1;
            var travelTagService = new Mock<ITravelTagService>();
            var cityService = new Mock<ICityService>();
            var expected = 1;
            using (var assertContext = new CarpoolingContext(this.options))
            {
                var sut = new TravelService(assertContext, travelTagService.Object, cityService.Object);
                sut.ApplyAsPassenger(userId, travelId);
                var actual = sut.Get(travelId).ApplyingPassengers.Count();
                Assert.AreEqual(expected, actual);
            }
        }
    }
}
