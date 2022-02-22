using Carpooling.Data;
using Carpooling.Services.Exceptions;
using Carpooling.Services.Services;
using Carpooling.Services.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Threading.Tasks;

namespace Carpooling.Tests.Tests.TravelTests
{
    [TestClass]
    public class AddPassenger_Should : BaseTest
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
        public void Throw_When_TravelIsComplete()
        {
            var userId = 5;
            var travelId = 2;
            var driverId = 2;
            var travelTagService = new Mock<ITravelTagService>();
            var cityService = new Mock<ICityService>();
            using (var assertContext = new CarpoolingContext(this.options))
            {
                var sut = new TravelService(assertContext, travelTagService.Object, cityService.Object);
                Assert.ThrowsException<TravelException>(() => sut.AddPassengerAsync(userId, driverId, travelId));
            }
        }

        [TestMethod]
        public void Throw_When_FreeSpotsAreZero()
        {
            var userId = 5;
            var travelId = 4;
            var driverId = 4;
            var travelTagService = new Mock<ITravelTagService>();
            var cityService = new Mock<ICityService>();
            using (var assertContext = new CarpoolingContext(this.options))
            {
                var sut = new TravelService(assertContext, travelTagService.Object, cityService.Object);
                Assert.ThrowsException<TravelException>(() => sut.AddPassengerAsync(userId, driverId, travelId));
            }
        }

        [TestMethod]
        public void Throw_When_DriverIsNotValid()
        {
            var userId = 5;
            var travelId = 3;
            var driverId = 1;
            var travelTagService = new Mock<ITravelTagService>();
            var cityService = new Mock<ICityService>();
            using (var assertContext = new CarpoolingContext(this.options))
            {
                var sut = new TravelService(assertContext, travelTagService.Object, cityService.Object);
                Assert.ThrowsException<TravelException>(() => sut.AddPassengerAsync(userId, driverId, travelId));
            }
        }

        [TestMethod]
        public void Throw_When_UserNotFound()
        {
            var userId = 14;
            var travelId = 3;
            var driverId = 3;
            var travelTagService = new Mock<ITravelTagService>();
            var cityService = new Mock<ICityService>();
            using (var assertContext = new CarpoolingContext(this.options))
            {
                var sut = new TravelService(assertContext, travelTagService.Object, cityService.Object);
                Assert.ThrowsException<EntityNotFoundException>(() => sut.AddPassengerAsync(userId, driverId, travelId));
            }
        }

        [TestMethod]
        public void Throw_When_UserNotApplied()
        {
            var userId = 5;
            var travelId = 3;
            var driverId = 3;
            var travelTagService = new Mock<ITravelTagService>();
            var cityService = new Mock<ICityService>();
            using (var assertContext = new CarpoolingContext(this.options))
            {
                var sut = new TravelService(assertContext, travelTagService.Object, cityService.Object);
                Assert.ThrowsException<EntityNotFoundException>(() => sut.AddPassengerAsync(userId, driverId, travelId));
            }
        }

        [TestMethod]
        public async Task AddUser_When_ParamsAreValid()
        {
            var userId = 5;
            var travelId = 3;
            var driverId = 3;
            var travelTagService = new Mock<ITravelTagService>();
            var cityService = new Mock<ICityService>();
            using (var assertContext = new CarpoolingContext(this.options))
            {
                var expected = 1;
                var sut = new TravelService(assertContext, travelTagService.Object, cityService.Object);
                await sut.ApplyAsPassengerAsync(userId, travelId);
                await sut.AddPassengerAsync(userId, driverId, travelId);
                var acutal = sut.Get(travelId).Passengers.Count();
                Assert.AreEqual(expected, acutal);
            }
        }
    }
}
