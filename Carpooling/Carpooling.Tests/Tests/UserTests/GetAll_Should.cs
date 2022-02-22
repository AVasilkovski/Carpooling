using Carpooling.Data;
using Carpooling.Services.DTOs;
using Carpooling.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace Carpooling.Tests.Tests.UserTests
{
    [TestClass]
    public class GetAll_Should : BaseTest
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
                arrangeContext.Feedbacks.AddRange(Utils.GetFeedbacks());
                arrangeContext.Travels.AddRange(Utils.GetTravels());
                arrangeContext.SaveChanges();
            }
        }

        [TestMethod]
        public async Task ReturnUser_When_ParamsAreValid()
        {
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

            var expected = user;

            using (var assertContext = new CarpoolingContext(this.options))
            {
                var sut = new UserService(assertContext);
                await sut.CreateAsync(user);
                var actual = sut.GetAll().First();
                Assert.AreEqual(expected.Username, actual.Username);
                Assert.AreEqual(expected.Email, actual.Email);
                Assert.AreEqual(expected.FirstName, actual.FirstName);
                Assert.AreEqual(expected.LastName, actual.LastName);
                Assert.AreEqual(expected.PhoneNumber, actual.PhoneNumber);
                Assert.AreEqual(expected.ProfilePicture, actual.ProfilePic);
            }
        }
    }
}
