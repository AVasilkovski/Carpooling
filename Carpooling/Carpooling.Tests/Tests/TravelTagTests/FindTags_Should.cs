using Carpooling.Data;
using Carpooling.Data.Models;
using Carpooling.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Carpooling.Tests.Tests.TravelTagTests
{
    [TestClass]
    public class FindTags_Should : BaseTest
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
                arrangeContext.TravelTags.AddRange(Utils.GetTravelTags());
                arrangeContext.SaveChanges();
            }
        }

        [TestMethod]
        public void ReturnTravelTags_When_ParamsAreValid()
        {
            var expectedTags = new List<TravelTag>()
            {

                new TravelTag()
                {
                    Id = 1,
                    Tag = "No smoking"
                },
                new TravelTag()
                {
                    Id = 2,
                    Tag = "No luggage"
                }
            };

            var tags = new List<string>()
            {
                "No smoking",
                "No luggage"
            };

            using (var assertContext = new CarpoolingContext(this.options))
            {
                var sut = new TravelTagService(assertContext);
                var actualTags = sut.FindTags(tags).ToList();
                for (int i = 0; i < expectedTags.Count; i++)
                {
                    var expected = expectedTags[i];
                    var actual = actualTags[i];
                    Assert.AreEqual(expected.Id, actual.Id);
                    Assert.AreEqual(expected.Tag, actual.Tag);
                }
            }
        }
    }
}
