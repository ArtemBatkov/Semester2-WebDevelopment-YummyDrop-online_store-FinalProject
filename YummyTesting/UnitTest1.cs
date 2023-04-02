using YummyDrop_online_store.Pages;

namespace YummyTesting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var counter = new Counter();
            var result = counter.AddTwoNumbers(1, 1);
            Assert.AreEqual(2, result);
        }
    }
}