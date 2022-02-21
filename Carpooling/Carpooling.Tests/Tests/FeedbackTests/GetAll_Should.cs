using Carpooling.Data;
using Carpooling.Data.Models.Enums;
using Carpooling.Services.DTOs;
using Carpooling.Services.Services;
using Carpooling.Services.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Carpooling.Tests.Tests.FeedbackTests
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
                arrangeContext.Travels.AddRange(Utils.GetTravels());
                arrangeContext.Feedbacks.AddRange(Utils.GetFeedbacks());
                arrangeContext.SaveChanges();
            }
        }

        [TestMethod]
        public void MyTestMethod()
        {
            var userService = new Mock<IUserService>();
            var expectedFeedbacks = new List<FeedbackPresentDTO>()
            {
                new FeedbackPresentDTO()
                {
                    Comment = "Amazing person. It was cool to travel with you.",
                    Rating = 4.9,
                    TravelId = 1,
                    Type = FeedbackType.Driver,
                },
                new FeedbackPresentDTO()
                {
                    Comment = "Drives very safely.",
                    Rating = 5,
                    TravelId = 1,
                    Type = FeedbackType.Passenger,
                }
            };

            using (var assertContext = new CarpoolingContext(options))
            {
                var sut = new FeedbackService(assertContext, userService.Object);
                var actualFeedbacks = sut.GetAll().Take(2).ToList();
                for (int i = 0; i < expectedFeedbacks.Count; i++)
                {
                    var expected = expectedFeedbacks[i];
                    var actual = actualFeedbacks[i];
                    Assert.AreEqual(expected.Comment, actual.Comment);
                    Assert.AreEqual(expected.Rating, actual.Rating);
                    Assert.AreEqual(expected.TravelId, actual.TravelId);
                    Assert.AreEqual(expected.Type, actual.Type);
                }
            }
        }
    }
}
