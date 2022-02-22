using Carpooling.Data;
using Carpooling.Services.DTOs;
using Carpooling.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Carpooling.Tests.Tests.UserTests
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
                arrangeContext.SaveChanges();
            }
        }

        [TestMethod]
        public void UpdateUser_When_ParamsAreValid()
        {
            int id = 1;
            var user = new UserCreateDTO()
            {
                Email = "kojo.t@gmail.com",
                FirstName = "Jorgi",
                LastName = "Jodorov",
                Password = "Randompazzz+12",
                PhoneNumber = "0923425032",
                ProfilePicture = "gosho232Profilepic.jpg"
            };

            var expected = user;

            using (var assertContext = new CarpoolingContext(this.options))
            {
                var sut = new UserService(assertContext);
                var actual = sut.UpdateAsync(id, user);
                Assert.AreEqual(expected.Email, actual.Result.Email);
                Assert.AreEqual(expected.FirstName, actual.Result.FirstName);
                Assert.AreEqual(expected.LastName, actual.Result.LastName);
                Assert.AreEqual(expected.PhoneNumber, actual.Result.PhoneNumber);
                Assert.AreEqual(expected.ProfilePicture, actual.Result.ProfilePic);
            }
        }
    }
}
