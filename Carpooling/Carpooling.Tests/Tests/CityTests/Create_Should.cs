using Carpooling.Data;
using Carpooling.Data.Models;
using Carpooling.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Carpooling.Tests.Tests.CityTests
{
    [TestClass]
    public class Create_Should : BaseTest
    {
        private DbContextOptions<CarpoolingContext> options;

        [TestInitialize]
        public void Initialize()
        {
            // Arrange
            this.options = Utils.GetOptions(nameof(TestContext.TestName));

            using (var arrangeContext = new CarpoolingContext(this.options))
            {
                arrangeContext.Travels.AddRange(Utils.GetTravels());
                arrangeContext.Cities.AddRange(Utils.GetCities());
                arrangeContext.SaveChanges();
            }
        }

        [TestMethod]
        public void ReturnCity_When_ParamsAreValid()
        {
            var cityName = "Pernik";
            var expected = new City()
            {
                Id = 7,
                Name = cityName
            };

            using (var assertContext = new CarpoolingContext(this.options))
            {
                var sut = new CityService(assertContext);
                var actual = sut.CreateCityAsync(cityName);
                Assert.AreEqual(expected.Id, actual.Id);
                Assert.AreEqual(expected.Name, actual.Result.Name);
            }
        }
    }
}
