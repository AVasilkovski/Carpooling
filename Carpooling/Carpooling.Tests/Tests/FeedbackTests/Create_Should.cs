//using Carpooling.Data;
//using Carpooling.Data.Models.Enums;
//using Carpooling.Services.DTOs;
//using Carpooling.Services.Services;
//using Carpooling.Services.Services.Contracts;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using System.Threading.Tasks;

//namespace Carpooling.Tests.Tests.FeedbackTests
//{
//    [TestClass]
//    public class Create_Should : BaseTest
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
//                arrangeContext.Travels.AddRange(Utils.GetTravels());
//                arrangeContext.Feedbacks.AddRange(Utils.GetFeedbacks());
//                arrangeContext.Roles.AddRange(Utils.GetRoles());
//                arrangeContext.SaveChanges();
//            }
//        }

//        [TestMethod]
//        public async Task CreateFeedback_When_ParamsAreValid()
//        {
//            var feedback = new FeedbackCreateDTO()
//            {
//                UserFromId = 14,
//                UserToId = 15,
//                Comment = "No",
//                Rating = 1,
//                TravelId = 1,
//                Type = FeedbackType.Driver
//            };
//            var user1 = new UserCreateDTO()
//            {
//                Username = "Gosho232",
//                Email = "kojo.t@gmail.com",
//                FirstName = "Jorgi",
//                LastName = "Jodorov",
//                Password = "Randompazzz+12",
//                PhoneNumber = "0923425032"
//            };
//            var user2 = new UserCreateDTO()
//            {
//                Username = "Gosho233",
//                Email = "kojou.t@gmail.com",
//                FirstName = "Jorgi",
//                LastName = "Jodorov",
//                Password = "Randompazzz+12",
//                PhoneNumber = "0923425932"
//            };
//            var userService = new Mock<IUserService>();
//            var expected = feedback;
//            using (var assertContext = new CarpoolingContext(options))
//            {
//                var sut = new FeedbackService(assertContext, userService.Object);
//                var userservice = new UserService(assertContext);
//                await userservice.CreateAsync(user1);
//                await userservice .CreateAsync(user2);
//                var actual = sut.CreateAsync(feedback);
//                Assert.AreEqual(expected.Comment, actual.Result.Comment);
//                Assert.AreEqual(expected.Rating, actual.Result.Rating);
//                Assert.AreEqual(expected.TravelId, actual.Result.TravelId);
//                Assert.AreEqual(expected.Type, actual.Result.Type);
//            }
//        }
//    }
//}