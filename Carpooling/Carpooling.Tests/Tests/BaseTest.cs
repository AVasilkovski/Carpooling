using Carpooling.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Carpooling.Tests.Tests
{
    public abstract class BaseTest
    {
        [TestCleanup]
        public void Cleanup()
        {
            var options = Utils.GetOptions(nameof(TestContext.TestName));
            var context = new CarpoolingContext(options);
            context.Database.EnsureDeleted();
        }
    }
}