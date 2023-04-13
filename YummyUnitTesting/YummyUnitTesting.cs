using Microsoft.Extensions.DependencyInjection;
using YummyDrop_online_store.Controllers;
using YummyDrop_online_store.Models;
using YummyDrop_online_store.Services.GeneratorService;
using YummyDrop_online_store.Services.RandomizeService;

namespace YummyUnitTesting
{
    [TestClass]
    public class YummyUnitTesting
    {
        private ServiceProvider _serviceProvider;
        private YummyAPIController? _contr;
        private GeneratorService _generatorService;
        private RandomizeSerivce _randomizeSerivce;

        [TestInitialize]
        public void TestInitialize()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IRandomizeService, RandomizeSerivce>();
            services.AddSingleton<IGeneratorService, GeneratorService>();
            services.AddSingleton<YummyAPIController>();

            _serviceProvider = services.BuildServiceProvider();
            _contr = _serviceProvider.GetService<YummyAPIController>();
            _generatorService = _serviceProvider.GetService<IGeneratorService>() as GeneratorService;
            _randomizeSerivce = _serviceProvider.GetService<IRandomizeService>() as RandomizeSerivce;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _serviceProvider.Dispose();
        }


        [TestMethod]
        public void GettingMillionIdsTest()
        {
            var yummys = YummyItemList.Yummys;
            var shuffledIds = _generatorService.GenerateMillionIds(yummys);
            int expectedSize = shuffledIds.Count;
            Assert.AreEqual(expectedSize, shuffledIds.Count);
        }

        [TestMethod]
        public void GetRandomItemTest()
        {
            var yummys = YummyItemList.Yummys;
            var shuffledIds = _generatorService.GenerateMillionIds(yummys);
            int randomId = _randomizeSerivce.GetRandomId(shuffledIds);
            Assert.IsNotNull(randomId);
        }

        [TestMethod]
        public void GetPredictedItemTest()
        {
            var random = new Random(1);
            var yummys = YummyItemList.Yummys;
            var shuffledIds = _generatorService.GenerateMillionIds(yummys);
            int randomId = _randomizeSerivce.GetRandomId(shuffledIds, true);
            int predict = random.Next(0, shuffledIds.Count);
            int predictedId = shuffledIds[predict];
            Assert.AreEqual(predictedId, randomId);
        }
    }
}