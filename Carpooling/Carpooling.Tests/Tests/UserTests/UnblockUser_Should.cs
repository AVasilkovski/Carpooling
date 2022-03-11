//using Carpooling.Data;
//using Carpooling.Data.Models.Enums;
//using Carpooling.Services.DTOs;
//using Carpooling.Services.Services;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Threading.Tasks;

//namespace Carpooling.Tests.Tests.UserTests
//{
//    [TestClass]
//    public class UnblockUser_Should : BaseTest
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
//        public async Task UnblockUser_When_ParamsAreValid()
//        {
//            var id = 9;
//            var expected = new UserPresentDTO()
//            {
//                Id = 9,
//                Username = "saintpaf",
//                FirstName = "Pavel",
//                LastName = "Krustev",
//                Email = "p.krustev@gmail.com",
//                PhoneNumber = "0977800152",
//                ProfilePictureName = "saintpafProfilepic.jpeg",
//                UserStatus = UserStatus.Active,
//                RatingAsDriver = 0,
//                RatingAsPassenger = 2.5
//            };

//            using (var assertContext = new CarpoolingContext(this.options))
//            {
//                var sut = new UserService(assertContext);
//                await sut.UnblockUserAsync(id);
//                var actual = sut.Get(id);
//                Assert.AreEqual(expected.Id, actual.Id);
//                Assert.AreEqual(expected.Username, actual.Username);
//                Assert.AreEqual(expected.FirstName, actual.FirstName);
//                Assert.AreEqual(expected.LastName, actual.LastName);
//                Assert.AreEqual(expected.Email, actual.Email);
//                Assert.AreEqual(expected.PhoneNumber, actual.PhoneNumber);
//                Assert.AreEqual(expected.ProfilePictureName, actual.ProfilePictureName);
//                Assert.AreEqual(expected.UserStatus, actual.UserStatus);
//                Assert.AreEqual(expected.RatingAsDriver, actual.RatingAsDriver);
//                Assert.AreEqual(expected.RatingAsPassenger, actual.RatingAsPassenger);
//            }
//        }
//    }
//}
