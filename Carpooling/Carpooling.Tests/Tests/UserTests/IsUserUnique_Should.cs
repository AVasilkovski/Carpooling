//using Carpooling.Data;
//using Carpooling.Services.Exceptions;
//using Carpooling.Services.Services;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace Carpooling.Tests.Tests.UserTests
//{
//    [TestClass]
//    public class IsUserUnique_Should : BaseTest
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
//        public void Throw_When_UsernameExists()
//        {
//            var username = "Gosho23";
//            using (var assertContext = new CarpoolingContext(this.options))
//            {
//                var sut = new UserService(assertContext);
//                Assert.ThrowsException<EntityAlreadyExistsException>(() => sut.IsUserUnique(username, null, null));
//            }
//        }

//        [TestMethod]
//        public void Throw_When_EmailExists()
//        {
//            var email = "gosho.t@gmail.com";

//            using (var assertContext = new CarpoolingContext(this.options))
//            {
//                var sut = new UserService(assertContext);
//                Assert.ThrowsException<EntityAlreadyExistsException>(() => sut.IsUserUnique(null, email, null));
//            }
//        }


//        [TestMethod]
//        public void Throw_When_PhoneNumberExists()
//        {
//            var phoneNumber = "0923425084";

//            using (var assertContext = new CarpoolingContext(this.options))
//            {
//                var sut = new UserService(assertContext);
//                Assert.ThrowsException<EntityAlreadyExistsException>(() => sut.IsUserUnique(null, null, phoneNumber));
//            }
//        }
//    }
//}