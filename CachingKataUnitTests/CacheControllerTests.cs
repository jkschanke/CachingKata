using CachingKataAPI.Controllers;

namespace CachingKataUnitTests
{
    [TestClass]
    public class CacheControllerTests
    {
        [TestMethod]
        public void AddAndRetrieveCachedItem()
        {
            var input = new CacheItem("one", "1", DateTime.Now.AddSeconds(1));
            var controller = new CacheController();
            controller.Add(input);
            var result = controller.Get("one");

            Assert.IsNotNull(result);
            Assert.AreEqual("1", result.Value);
        }

        [TestMethod]
        public void ReplaceCachedItem()
        {
            var input = new CacheItem("one", "1", DateTime.Now.AddSeconds(1));
            var controller = new CacheController();
            controller.Add(input);
            var input2 = new CacheItem("two", "2", DateTime.Now.AddSeconds(1));
            controller.Replace(input2);

            var result = controller.Get("two");

            Assert.IsNotNull(result);
            Assert.AreEqual("2", result.Value);
        }

        [TestMethod]
        public void ExpiredItemDoesNotReturn()
        {
            var input = new CacheItem("one", "1", DateTime.Now.AddSeconds(2));
            var controller = new CacheController();
            controller.Add(input);
            Thread.Sleep(4000);
            var result = controller.Get("one");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void PurgeCachedItem()
        {
            var input = new CacheItem("one", "1", DateTime.Now.AddSeconds(2));
            var controller = new CacheController();
            controller.Add(input);
            var result = controller.Get("one");

            Assert.IsNotNull(result);

            controller.Purge("one");
            result = controller.Get("one");
            Assert.IsNull(result);
        }
    }
}