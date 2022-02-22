using Carpooling.Data;
using Carpooling.Services.Services;
using Carpooling.Services.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace Carpooling.Tests.Tests.TravelTests
{
    [TestClass]
    public class Update_Should : BaseTest
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
        public async Task CreateTravel_When_ParamsAreValid()
        {
            var id = 1;
            var travelTagService = new Mock<ITravelTagService>();
            var cityService = new Mock<ICityService>();
            var travel = new Services.DTOs.TravelCreateDTO()
            {
                DepartureTime = new DateTime(2024, 7, 26, 14, 30, 00),
                FreeSpots = 5,
            };

            var expected = travel;

            using (var assertContext = new CarpoolingContext(this.options))
            {

                var sut = new TravelService(assertContext, travelTagService.Object, cityService.Object);
                var actual = await sut.UpdateAsync(id, travel);
                Assert.AreEqual(expected.DepartureTime, actual.DepartureTime);
                Assert.AreEqual(expected.FreeSpots, actual.FreeSpots);
            }
        }
    }
}
