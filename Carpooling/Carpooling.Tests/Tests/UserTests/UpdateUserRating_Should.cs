//using Carpooling.Data;
//using Carpooling.Data.Models.Enums;
//using Carpooling.Services.Services;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Threading.Tasks;

//namespace Carpooling.Tests.Tests.UserTests
//{
//    [TestClass]
//    public class UpdateUserRating_Should : BaseTest
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
//                arrangeContext.Feedbacks.AddRange(Utils.GetFeedbacks());
//                arrangeContext.Travels.AddRange(Utils.GetTravels());
//                arrangeContext.SaveChanges();
//            }
//        }

//        [TestMethod]
//        public async Task CalculateRatingAsPassenger_When_ParamsAreValid()
//        {
//            int userId = 1;
//            var feedbackType = FeedbackType.Driver;
//            var expected = 5;
//            using (var assertContext = new CarpoolingContext(this.options))
//            {
//                var sut = new UserService(assertContext);
//                await sut.UpdateUserRatingAsync(userId, feedbackType);
//                var actual = sut.Get(userId);
//                Assert.AreEqual(expected, actual.RatingAsPassenger);
//            }
//        }

//        [TestMethod]
//        public async Task CalculateRatingAsDriver_When_ParamsAreValid()
//        {

//            int userId = 1;
//            var feedbackType = FeedbackType.Passenger;
//            var expected = 5;
//            using (var assertContext = new CarpoolingContext(this.options))
//            {
//                var sut = new UserService(assertContext);
//                await sut.UpdateUserRatingAsync(userId, feedbackType);
//                var actual = sut.Get(userId);
//                Assert.AreEqual(expected, actual.RatingAsDriver);
//            }
//        }
//    }
//}
