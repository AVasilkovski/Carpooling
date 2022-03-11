//using Carpooling.Data;
//using Carpooling.Services.DTOs;
//using Carpooling.Services.Services;
//using Carpooling.Services.Services.Contracts;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Carpooling.Tests.Tests.TravelTests
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
//                arrangeContext.Roles.AddRange(Utils.GetRoles());
//                arrangeContext.Travels.AddRange(Utils.GetTravels());
//                arrangeContext.Feedbacks.AddRange(Utils.GetFeedbacks());
//                arrangeContext.Cities.AddRange(Utils.GetCities());
//                arrangeContext.TravelTags.AddRange(Utils.GetTravelTags());
//                arrangeContext.SaveChanges();
//            }
//        }

//        [TestMethod]
//        public async Task CreateTravel_When_ParamsAreValid()
//        {
//            var travelTagService = new Mock<ITravelTagService>();
//            var cityService = new Mock<ICityService>();
//            var travel = new TravelCreateDTO()
//            {
//                StartPointAddress = new CityCreateDTO() { City = "random" },
//                EndPointAddress = new CityCreateDTO() { City = "random" },
//                TravelTags = new List<string>() { "random" },
//                DepartureTime = new DateTime(2024, 7, 26, 14, 30, 00),
//                FreeSpots = 5,
//                DriverId = 14,
//            };
//            var user = new UserCreateDTO()
//            {
//                Username = "Gosho232",
//                Email = "kojo.t@gmail.com",
//                FirstName = "Jorgi",
//                LastName = "Jodorov",
//                Password = "Randompazzz+12",
//                PhoneNumber = "0923425032",
//            };

//            var expected = travel;

//            using (var assertContext = new CarpoolingContext(this.options))
//            {
//                travelTagService.Setup(tagService => tagService.FindTags(It.IsAny<IEnumerable<string>>()))
//                                .Returns(assertContext.TravelTags.ToList());
//                cityService.SetupSequence(cities => cities.CheckIfCityExistAsync(It.IsAny<string>()))
//                           .Returns(assertContext.Cities.Take(1).FirstAsync())
//                           .Returns(assertContext.Cities.Skip(1).Take(1).FirstAsync());

//                var sut = new TravelService(assertContext, travelTagService.Object, cityService.Object);
//                var userService = new UserService(assertContext);
//                await userService.CreateAsync(user);
//                var actual = await sut.CreateAsync(travel);
//                Assert.AreEqual(expected.DepartureTime, actual.DepartureTime);
//                Assert.AreEqual(expected.FreeSpots, actual.FreeSpots);
//            }
//        }
//    }
//}
