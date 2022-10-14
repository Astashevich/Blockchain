using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blockchain.Tests
{
    [TestClass()]
    public class ChainTests
    {
        [TestMethod()]
        public void ChainTest()
        {
            Chain chain = new();
            chain.Add("Arseniy", "Admin");

            Assert.AreEqual("Arseniy", chain.Last.Data);
        }

        [TestMethod()]
        public void CheckTest()
        {
            var chain = new Chain();
            chain.Add("Hello, Friends!", "Admin");
            chain.Add("test", "User");

            Assert.IsTrue(chain.Check());
        }
    }
}