using DbContextSharLab;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Diagnostics;
using YummyDrop_online_store.Controllers;
using YummyDrop_online_store.Services.ClientService;
using YummyDrop_online_store.Services.GeneratorService;
using YummyDrop_online_store.Services.RandomizeService;
using YummySharedLibrary;

namespace YummyTesting
{
    [TestClass]
    public class TestServices
    {

        private ServiceProvider _serviceProvider;
        private YummyAPIController? _contr;
        
        private Random random = new Random();

        //[TestInitialize]
        //public void TestInitialize()
        //{
        //    var services = new ServiceCollection();
        //    services.AddSingleton<IRandomizeService, RandomizeSerivce>();
        //    services.AddSingleton<IGeneratorService, GeneratorService>();
        //    services.AddSingleton<YummyAPIController>();

        //    _serviceProvider = services.BuildServiceProvider();
        //    _contr = _serviceProvider.GetService<YummyAPIController>();
        //    _generatorService = _serviceProvider.GetService<GeneratorService>();
        //}
       
        private static List<YummyItem> LootList;

        private static IGeneratorService _generatorService;
        private static IRandomizeService _randomizeSerivce;

        private static IServiceProvider ServiceProvider { get; set; }
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            var services = new ServiceCollection();
            services.AddSingleton<IRandomizeService, RandomizeSerivce>();
            services.AddSingleton<IGeneratorService, GeneratorService>();
            services.AddSingleton<YummyAPIController>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=FruitBoxTable;Trusted_Connection=True;")
                .Options;

            var dbContext = new ApplicationDbContext(options);
            dbContext.Database.EnsureCreated();


            ServiceProvider = services.BuildServiceProvider();

            var boxes = dbContext.FruitBoxTable.Include(box => box.BoxContent1).ToListAsync().GetAwaiter().GetResult();
            var box1 = boxes[0];
            var boxcont = box1.BoxContent1;
            LootList = boxcont;
            _generatorService = ServiceProvider.GetService<IGeneratorService>() as GeneratorService;
            _randomizeSerivce = ServiceProvider.GetService<IRandomizeService>() as RandomizeSerivce;
        }


        [TestCleanup]
        public void TestCleanup()
        {
            ServiceProvider = null;
        }



        [TestMethod]
        public async Task TestAPIGetRandromYummyItemStatusCode()
        {
            var shuffledIds = _generatorService.GenerateMillionIds(LootList);
            var url = "http://localhost:5179/api/yummy";
            ClientService client = new ClientService();
            HttpResponseMessage response = await client.FetchDataFromAPI(url);
            Assert.IsTrue(response.IsSuccessStatusCode);
        }




        [TestMethod]
        public async Task APIReturnsYummyItemTest()
        {
            var url = "http://localhost:5179/api/yummy";
            var client = new ClientService();
            var response = await client.FetchDataFromAPI(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var expected = JsonConvert.DeserializeObject<YummyItem>(json);
                Assert.IsNotNull(expected);
            }
            else
            {
                Assert.Fail($"NOT SUCCESSFULL STATUS CODE! \n code: {response.StatusCode}");
            }
        }



        






    }
}