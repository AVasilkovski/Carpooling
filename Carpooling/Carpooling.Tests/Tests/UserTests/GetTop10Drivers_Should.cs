//using Carpooling.Data;
//using Carpooling.Services.Services;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Linq;

//namespace Carpooling.Tests.Tests.UserTests
//{
//    [TestClass]
//    public class GetTop10Drivers_Should : BaseTest
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
//        public void ReturnCorrectDrivers_When_ParamsAreValid()
//        {
//            using (var assertContext = new CarpoolingContext(this.options))
//            {
//                var expectedPassengers = assertContext.Users.Where(user => user.RatingAsDriver > 0)
//                                                            .OrderByDescending(user => user.RatingAsDriver)
//                                                            .Take(10)
//                                                            .ToList();

//                var sut = new UserService(assertContext);
//                var actualPassengers = sut.GetTop10Drivers().ToList();
//                for (int i = 0; i < expectedPassengers.Count; i++)
//                {
//                    var expected = expectedPassengers[i];
//                    var actual = actualPassengers[i];
//                    Assert.AreEqual(expected.Id, actual.Id);
//                    Assert.AreEqual(expected.Username, actual.Username);
//                    Assert.AreEqual(expected.FirstName, actual.FirstName);
//                    Assert.AreEqual(expected.LastName, actual.LastName);
//                    Assert.AreEqual(expected.Email, actual.Email);
//                    Assert.AreEqual(expected.PhoneNumber, actual.PhoneNumber);
//                    Assert.AreEqual(expected.ProfilePictureName, actual.ProfilePictureName);
//                    Assert.AreEqual(expected.UserStatus, actual.UserStatus);
//                    Assert.AreEqual(expected.RatingAsDriver, actual.RatingAsDriver);
//                    Assert.AreEqual(expected.RatingAsPassenger, actual.RatingAsPassenger);
//                }
//            }
//        }
//    }
//}
