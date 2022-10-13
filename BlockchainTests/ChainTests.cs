using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blockchain.Tests
{
    [TestClass()]
    public class ChainTests
    {
        [TestMethod()]
        public void ChainTest()
        {
            Chain chain = new Chain();
            chain.Add("Arseniy", "Admin");

            Assert.AreEqual(2, chain.Blocks.Count);
            Assert.AreEqual("Arseniy", chain.Last.Data);
        }
    }
}