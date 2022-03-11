//using Carpooling.Data;
//using Carpooling.Services.DTOs;
//using Carpooling.Services.Services;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Carpooling.Tests.Tests.UserTests
//{
//    [TestClass]
//    public class GetFilteredUsers_Should : BaseTest
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
//        public async Task ReturnUsers_When_ParamsAreValidAsync()
//        {
//            var user1 = new UserCreateDTO()
//            {
//                Username = "Gosho232",
//                Email = "kojo.t@gmail.com",
//                FirstName = "Jorgi",
//                LastName = "Jodorov",
//                PhoneNumber = "0923425032",
//                ProfilePictureName = "defaultProfilePicture.png"
//            };
//            var user2 = new UserCreateDTO()
//            {
//                Username = "prowrestler112",
//                FirstName = "Rey",
//                LastName = "Misterio",
//                Email = "misteriowrestler@gmail.com",
//                Password = "Bigmistery*3",
//                PhoneNumber = "0861433101",
//                ProfilePictureName = "defaultProfilePicture.png",
//            };
//            var expectedUsers = new List<UserCreateDTO>()
//            {
//                user1,
//                user2
//            };

//            using (var assertContext = new CarpoolingContext(this.options))
//            {
//                var sut = new UserService(assertContext);
//                await sut.CreateAsync(user1);
//                await sut.CreateAsync(user2);
//                var actualUsers = sut.GetFilteredUsers("0", "o", "o").ToList();
//                for (int i = 0; i < expectedUsers.Count; i++)
//                {
//                    var expected = expectedUsers[i];
//                    var actual = actualUsers[i];
//                    Assert.AreEqual(expected.Username, actual.Username);
//                    Assert.AreEqual(expected.Email, actual.Email);
//                    Assert.AreEqual(expected.FirstName, actual.FirstName);
//                    Assert.AreEqual(expected.LastName, actual.LastName);
//                    Assert.AreEqual(expected.PhoneNumber, actual.PhoneNumber);
//                    Assert.AreEqual(expected.ProfilePictureName, actual.ProfilePictureName);
//                }
//            }
//        }
//    }
//}
