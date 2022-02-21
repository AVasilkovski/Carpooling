using Carpooling.Data.Models.Enums;
using Carpooling.Services.DTOs;
using Carpooling.Services.Exceptions;
using Carpooling.Services.Services.Contracts;
using Carpooling.Web.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Carpooling.Tests.Tests.AuthTests
{
    [TestClass]
    public class TryGetUser_Should : BaseTest
    {
        [TestMethod]

        public void ReturnCorrectUser_When_ParamsAreValid()
        {
            var user = new UserPresentDTO()
            {
                Id = 22,
                Username = "TestUser",
                FirstName = "First Name",
                LastName = "Last Name",
                Email = "a@abv.bg",
                Status = UserStatus.Active
            };

            var expected = user;
            var userService = new Mock<IUserService>();
            userService.Setup(userService => userService.GetUserByCredentials(It.IsAny<string>(), It.IsAny<string>()))
                       .Returns(user);

            var sut = new AuthHelper(userService.Object);

            var actual = sut.TryGetUser("TestUser", "Password1*8");

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Username, actual.Username);
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.Status, actual.Status);
        }

        [TestMethod]

        public void Throw_When_FeedbackNotFound()
        {
            string nonExistingCredentials = "f";
            string nonExistingPass = "l";
            var userService = new Mock<IUserService>();
            userService.Setup(service => service.GetUserByCredentials(It.IsAny<string>(), It.IsAny<string>()))
                       .Throws(new EntityNotFoundException());

            var sut = new AuthHelper(userService.Object);
            Assert.ThrowsException<EntityNotFoundException>(() => sut.TryGetUser(nonExistingCredentials, nonExistingPass));
        }
    }
}