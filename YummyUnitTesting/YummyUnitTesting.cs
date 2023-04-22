using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using YummyDrop_online_store.Controllers;
using YummyDrop_online_store.Data;

using YummyDrop_online_store.Services.GeneratorService;
using YummyDrop_online_store.Services.RandomizeService;

using Microsoft.EntityFrameworkCore.InMemory;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoreLinq;
using System.Diagnostics;



//using NUnit.Framework;

using YummySharedLibrary;
using DbContextSharLab;
using YummyDrop_online_store.Services.CartService;
using YummyDrop_online_store.Services.BonusService;
using YummyDrop_online_store.Services.BalanceService;
using Assert = NUnit.Framework.Assert;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

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

        private CartService _cart;
        private BonusService _bonus;
        private BalanceService _balance;
        
        private static IServiceProvider ServiceProvider { get; set; }

        private static string filePath;

        [AssemblyInitialize]
        public static void AssemblyInitialize(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext context)
        {
            
            var services = new ServiceCollection();
            services.AddSingleton<IRandomizeService, RandomizeSerivce>();
            services.AddSingleton<IGeneratorService, GeneratorService>();
            services.AddSingleton<YummyAPIController>();
            
            
            services.AddSingleton<IBalanceService, BalanceService>();
            services.AddSingleton<IBonusService, BonusService>();
            services.AddSingleton<ICartService, CartService>();

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
            
            _cart = ServiceProvider.GetService<ICartService>() as CartService;
            _bonus = ServiceProvider.GetService<IBonusService>() as BonusService;
            _balance = ServiceProvider.GetService<IBalanceService>() as BalanceService;
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

        

        [TestMethod]
        [Priority (0)]// add to cart- 0 priority
        public void TestCartAddings()
        {
            var shuffledIds = _generatorService.GenerateMillionIds(LootList);
            int randomId = _randomizeSerivce.GetRandomId(shuffledIds);
            YummyItem yummyItem = LootList.FirstOrDefault(i => i.Id == randomId);
            Assert.IsNotNull(yummyItem);
            var initialCartLen = _cart.GetAllCartObjects().Count();
            _cart.addToCart(yummyItem);
            var currentCartLen = _cart.GetAllCartObjects().Count();
            Assert.AreEqual(1, currentCartLen - initialCartLen);
            //TestCartRemoveItem();
        }

        [TestMethod]
        [Priority(1)]// delete from cart - 1 priority
        public void TestCartRemoveItem()
        {
            var items = _cart.GetAllCartObjects();
            var initialCartLen = items.Count();
            Assert.IsTrue(initialCartLen > 0);
            var yummyItem = items[0];
            _cart.RemoveFromCart(yummyItem);
            var currentCartLen = _cart.GetAllCartObjects().Count();
            Assert.IsTrue(currentCartLen < initialCartLen);
        }

        [TestMethod]
        public void TestBalanceIncreased()
        {
            var initialBalance = _balance.Balance;
            var shuffledIds = _generatorService.GenerateMillionIds(LootList);
            int randomId = _randomizeSerivce.GetRandomId(shuffledIds);
            YummyItem yummyItem = LootList.FirstOrDefault(i => i.Id == randomId);
            _cart.addToCart(yummyItem);
            var cost = yummyItem.Cost;
            _cart.RemoveFromCart(yummyItem);
            _balance.AddToBalance(cost);
            var currentBalance = _balance.Balance;
            Assert.IsTrue(currentBalance > initialBalance);
        }

        [TestMethod]
        public void TestBalanceDecreased()
        {
            var initialBalance = _balance.Balance;
            var shuffledIds = _generatorService.GenerateMillionIds(LootList);
            int randomId = _randomizeSerivce.GetRandomId(shuffledIds);
            YummyItem yummyItem = LootList.FirstOrDefault(i => i.Id == randomId);
            var caseCost = 15;
            _balance.RemoveFromBalance(caseCost);
            var currentBalance = _balance.Balance;
            Assert.IsTrue(currentBalance < initialBalance);
        }

        [TestMethod]
        public void TestBonusIncreased()
        {
            var initialBonus = _bonus.Bonus;
            var shuffledIds = _generatorService.GenerateMillionIds(LootList);
            int randomId = _randomizeSerivce.GetRandomId(shuffledIds);
            YummyItem yummyItem = LootList.FirstOrDefault(i => i.Id == randomId);
            var caseBonus = 10;
            _bonus.AddBonuses(caseBonus);
            var currentBonus = _bonus.Bonus;
            Assert.IsTrue(currentBonus > initialBonus);
        }

        [TestMethod]
        public void TestBonusDecreased()
        {
            _bonus.RestartBonusDeposit();
            var initialBonus = _bonus.Bonus;
            var shuffledIds = _generatorService.GenerateMillionIds(LootList);
            int randomId = _randomizeSerivce.GetRandomId(shuffledIds);
            YummyItem yummyItem = LootList.FirstOrDefault(i => i.Id == randomId);
            var caseCostBonus = 100;
            _bonus.RemoveBonuses(caseCostBonus);
            var currentBonus = _bonus.Bonus;
            Assert.IsTrue(currentBonus < initialBonus);
        }
 

        
    }
}