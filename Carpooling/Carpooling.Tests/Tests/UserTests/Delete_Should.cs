using Carpooling.Data;
using Carpooling.Data.Models.Enums;
using Carpooling.Services.DTOs;
using Carpooling.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Carpooling.Tests.Tests.UserTests
{
    [TestClass]
    public class Delete_Should : BaseTest
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
                arrangeContext.SaveChanges();
            }
        }

        [TestMethod]
        public async Task DeleteUser_When_ParamsAreValid()
        {
            var id = 15;
            var user = new UserCreateDTO()
            {
                Username = "Gosho232",
                Email = "kojo.t@gmail.com",
                FirstName = "Jorgi",
                LastName = "Jodorov",
                Password = "Randompazzz+12",
                PhoneNumber = "0923425032",
                ProfilePicture = "defaultProfilePicture.png"
            };

            var expected = new UserPresentDTO()
            {
                Id = 15,
                Username = "Gosho232",
                Email = "kojo.t@gmail.com",
                FirstName = "Jorgi",
                LastName = "Jodorov",
                PhoneNumber = "0923425032",
                ProfilePic = "defaultProfilePicture.png",
                Status = UserStatus.Active,
                RatingAsPassenger = 0,
                RatingAsDriver = 0
            };

            using (var assertContext = new CarpoolingContext(this.options))
            {
                var sut = new UserService(assertContext);
                await sut.CreateAsync(user);
                var actual = await sut.DeleteAsync(id);
                Assert.AreEqual(expected.Id, actual.Id);
                Assert.AreEqual(expected.Username, actual.Username);
                Assert.AreEqual(expected.FirstName, actual.FirstName);
                Assert.AreEqual(expected.LastName, actual.LastName);
                Assert.AreEqual(expected.Email, actual.Email);
                Assert.AreEqual(expected.PhoneNumber, actual.PhoneNumber);
                Assert.AreEqual(expected.ProfilePic, actual.ProfilePic);
                Assert.AreEqual(expected.Status, actual.Status);
                Assert.AreEqual(expected.RatingAsDriver, actual.RatingAsDriver);
                Assert.AreEqual(expected.RatingAsPassenger, actual.RatingAsPassenger);
            }
        }
    }
}
