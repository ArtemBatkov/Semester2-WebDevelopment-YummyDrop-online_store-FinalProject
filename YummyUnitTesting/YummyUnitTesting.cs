using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using YummyDrop_online_store.Controllers;
using YummyDrop_online_store.Data;
using YummyDrop_online_store.Models;
using YummyDrop_online_store.Services.GeneratorService;
using YummyDrop_online_store.Services.RandomizeService;

using Microsoft.EntityFrameworkCore.InMemory;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoreLinq;
using System.Diagnostics;

namespace YummyUnitTesting
{
    [TestClass]
    public class YummyUnitTesting
    {
        //private ServiceProvider _serviceProvider;
        private YummyAPIController? _contr;
        private GeneratorService _generatorService;
        private RandomizeSerivce _randomizeSerivce;

        private ApplicationDbContext _dbcontext;

        private static  List<YummyItem> LootList;

        //[TestInitialize]
        //public  async Task TestInitialize()
        //{
        //    var services = new ServiceCollection();
        //    services.AddSingleton<IRandomizeService, RandomizeSerivce>();
        //    services.AddSingleton<IGeneratorService, GeneratorService>();
        //    services.AddSingleton<YummyAPIController>();


        //    _serviceProvider = services.BuildServiceProvider();
        //    _contr = _serviceProvider.GetService<YummyAPIController>();
        //    _generatorService = _serviceProvider.GetService<IGeneratorService>() as GeneratorService;
        //    _randomizeSerivce = _serviceProvider.GetService<IRandomizeService>() as RandomizeSerivce;


        //    var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        //    .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=FruitBoxTable;Trusted_Connection=True;")
        //    .Options;



        //    var dbContext = new ApplicationDbContext(options);

        //    _dbcontext = dbContext;
        //    await _dbcontext.Database.EnsureCreatedAsync();
        //    var boxes = await _dbcontext.FruitBoxTable.Include(box => box.BoxContent1).ToListAsync();
        //    var box1 = boxes[0];
        //    var boxcont = box1.BoxContent1;
        //    LootList = boxcont;
        //}

        //[TestCleanup]
        //public void TestCleanup()
        //{
        //    _serviceProvider.Dispose();
        //}
        //private static IServiceProvider _serviceProvider;
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
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _generatorService = ServiceProvider.GetService<IGeneratorService>() as GeneratorService;
            _randomizeSerivce = ServiceProvider.GetService<IRandomizeService>() as RandomizeSerivce;
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Dispose the DbContext
            //_serviceProvider.GetService<ApplicationDbContext>().Dispose();
            //ServiceProvider.GetService<ApplicationDbContext>().Dispose();
            
             
            var randomizeService = ServiceProvider.GetService<IRandomizeService>() as IDisposable;
            if (randomizeService != null)
            {
                randomizeService.Dispose();
            }
            var generateService = ServiceProvider.GetService<IGeneratorService>() as IDisposable;
            if (generateService != null)
            {
                generateService.Dispose();
            }
            var dbcontext = ServiceProvider.GetService<ApplicationDbContext>() as ApplicationDbContext;

            if (dbcontext != null)
            {
                dbcontext.Dispose();
            }
           
        }

    


        [TestMethod]
        public void GettingMillionIdsTest()
        {           
            var shuffledIds = _generatorService.GenerateMillionIds(LootList);
            int expectedSize = shuffledIds.Count;
            Assert.AreEqual(expectedSize, shuffledIds.Count);
        }

        [TestMethod]
        public void GetRandomItemTest()
        {             
            var shuffledIds = _generatorService.GenerateMillionIds(LootList);
            int randomId = _randomizeSerivce.GetRandomId(shuffledIds);
            Assert.IsNotNull(randomId);
        }

        [TestMethod]
        public void GetPredictedItemTest()
        {
            var random = new Random(1);             
            var shuffledIds = _generatorService.GenerateMillionIds(LootList);
            int randomId = _randomizeSerivce.GetRandomId(shuffledIds, true);
            int predict = random.Next(0, shuffledIds.Count);
            int predictedId = shuffledIds[predict];
            Assert.AreEqual(predictedId, randomId);
        }

        [TestMethod]
        public void DropErrorSatisfyTest()
        {
            int numberOfTest = 1_000_000;
            //initialize dictionary
            Dictionary<int, int> Statistics = new Dictionary<int, int>();
            double TotalChance = 0;
            //foreach (var item in LootList)
            //{
            //    Statistics[item.Id] = 0;
            //    TotalChance += item.DropChance;
            //}
            int listSize = LootList.Count;
            for (int i = 0; i <  listSize; i++)
            {
                var item = LootList[i];
                Statistics[item.Id] = 0;
                TotalChance += item.DropChance;
            }

            //test N times
            int[] shuffledIds = _generatorService.GenerateMillionIds(LootList.ToArray());
            int randomId;
            var random = new Random();
            for (int i = 0; i < numberOfTest; i++)
            {                
                randomId = _randomizeSerivce.GetRandomId(shuffledIds);
                Statistics[randomId]++; //collect statistic
                shuffledIds.Shuffle();
            }

            //Compute error
            int size = Statistics.Sum(x => x.Value);         
            double k = 1.96;
            double p, n, SE, ME, real, difference;
            
            //foreach(var item in LootList)
            for (int i = 0; i < listSize; i++)
            {
                var item = LootList[i];
                k = 1.96; //trusted interval is constant
                p = (double) item.DropChance / TotalChance ; //possible chance
                n = Statistics[item.Id]; //quantity in the dict

                SE = Math.Sqrt(p * (1 - p) / n); //Standard Error
                ME = SE * k;// max error

                //difference between real and theoretical
                real = (double)(n / size);
                difference = Math.Abs(real - p);

                //Debug.WriteIf(difference > ME, $"diff: {difference};  ME: {ME}");
                Debug.WriteLine($"diff: {difference} {(difference <= ME ? "<=" : ">")} ME: {ME}");
                double treshold = 0.5;//stop val
                if(difference > ME ) { 
                    var d = Math.Abs(difference -  ME);
                    Debug.WriteLine($"d: {d} {(d < treshold ? "<" : ">")} treshold: {treshold}");
                    Assert.IsTrue(d < treshold);
                }
                else {
                    Assert.IsTrue(difference <= ME);
                }
            }
        }



    }
}