//using Carpooling.Data;
//using Carpooling.Data.Models.Enums;
//using Carpooling.Services.DTOs;
//using Carpooling.Services.Exceptions;
//using Carpooling.Services.Services;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace Carpooling.Tests.Tests.UserTests
//{
//    [TestClass]
//    public class GetUserByCredentials_Should : BaseTest
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
//        public void ReturnCorrectUser_When_ParamsAreValid()
//        {
//            var username = "Gosho23";
//            var password = "Randompass+12";
//            var expected = new UserPresentDTO()
//            {
//                Id = 1,
//                Username = "Gosho23",
//                FirstName = "Georgi",
//                LastName = "Todorov",
//                Email = "gosho.t@gmail.com",
//                PhoneNumber = "0923425084",
//                ProfilePictureName = "goshProfilePic.jpeg",
//                UserStatus = UserStatus.Active,
//                RatingAsPassenger = 5,
//                RatingAsDriver = 5
//            };

//            using (var assertContext = new CarpoolingContext(this.options))
//            {
//                var sut = new UserService(assertContext);
//                var actual = sut.GetUserByCredentialsAsync(username, password);
//                Assert.AreEqual(expected.Id, actual.Id);
//                Assert.AreEqual(expected.Username, actual.us);
//                Assert.AreEqual(expected.FirstName, actual.FirstName);
//                Assert.AreEqual(expected.LastName, actual.LastName);
//                Assert.AreEqual(expected.Email, actual.Email);
//                Assert.AreEqual(expected.PhoneNumber, actual.PhoneNumber);
//                Assert.AreEqual(expected.ProfilePictureName, actual.ProfilePictureName);
//                Assert.AreEqual(expected.UserStatus, actual.Status);
//                Assert.AreEqual(expected.RatingAsDriver, actual.RatingAsDriver);
//                Assert.AreEqual(expected.RatingAsPassenger, actual.RatingAsPassenger);
//            }
//        }

//        [TestMethod]
//        public void Thorw_When_UserNotFound()
//        {
//            var username = "Gosho232";
//            var password = "Randompazz+12";
//            using (var assertContext = new CarpoolingContext(this.options))
//            {
//                var sut = new UserService(assertContext);
//                Assert.ThrowsException<EntityNotFoundException>(() => sut.GetUserByCredentialsAsync(username, password));
//            }
//        }
//    }
//}
