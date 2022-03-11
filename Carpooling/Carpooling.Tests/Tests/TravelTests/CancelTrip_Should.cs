//using Carpooling.Data;
//using Carpooling.Services.Exceptions;
//using Carpooling.Services.Services;
//using Carpooling.Services.Services.Contracts;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using System.Threading.Tasks;

//namespace Carpooling.Tests.Tests.TravelTests
//{
//    [TestClass]
//    public class CancelTrip_Should : BaseTest
//    {
//        private DbContextOptions<CarpoolingContext> options;

//        [TestInitialize]
//        public void Initialize()
//        {
//            // Arrange
//            this.options = Utils.GetOptions(nameof(TestContext.TestName));

//            using (var arrangeContext = new CarpoolingContext(this.options))
//            {
//                arrangeContext.Users.AddRange(Utils.GetUsers());
//                arrangeContext.Roles.AddRange(Utils.GetRoles());
//                arrangeContext.Travels.AddRange(Utils.GetTravels());
//                arrangeContext.Feedbacks.AddRange(Utils.GetFeedbacks());
//                arrangeContext.Cities.AddRange(Utils.GetCities());
//                arrangeContext.TravelTags.AddRange(Utils.GetTravelTags());
//                arrangeContext.SaveChanges();
//            }
//        }

//        [TestMethod]
//        public async Task CancelTripCorrectly_When_ParamsAreValid()
//        {
//            var travelId = 1;
//            var travelTagService = new Mock<ITravelTagService>();
//            var cityService = new Mock<ICityService>();
//            using (var assertContext = new CarpoolingContext(this.options))
//            {
//                var sut = new TravelService(assertContext, travelTagService.Object, cityService.Object);
//                await sut.CancelTripAsync(travelId);
//                Assert.ThrowsException<EntityNotFoundException>(() => sut.Get(travelId));
//            }
//        }
//    }
//}
