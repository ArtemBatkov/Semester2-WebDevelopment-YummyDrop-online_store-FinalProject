using YummyDrop_online_store.Pages;

namespace YummyTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AddNumbersTest()
        {
            var counter = new Counter();
            var result = counter.AddTwoNumbers(1, 1);
            Assert.AreEqual(29, result);
        }
    }
}