using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Diagnostics;
using YummyDrop_online_store.Controllers;
using YummyDrop_online_store.Models;
using YummyDrop_online_store.Services.ClientService;
using YummyDrop_online_store.Services.GeneratorService;
using YummyDrop_online_store.Services.RandomizeService;

namespace YummyTesting
{
    [TestClass]
    public class TestServices
    {

        private ServiceProvider _serviceProvider;
        private YummyAPIController? _contr;


        [TestInitialize]
        public void TestInitialize()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IRandomizeService, RandomizeSerivce>();
            services.AddSingleton<IGeneratorService, GeneratorService>();
            services.AddSingleton<YummyAPIController>();

            _serviceProvider = services.BuildServiceProvider();
            _contr = _serviceProvider.GetService<YummyAPIController>();

        }

        [TestCleanup]
        public void TestCleanup()
        {
            _serviceProvider.Dispose();
        }



        //[TestMethod]
        //public async Task TestAPIGetRandromYummyItemStatusCode()
        //{
        //    var service = new GeneratorService();
        //    var random = new Random();
        //    var items = service.GenerateYummyItemsList();
        //    var url = "http://localhost:5179/api/yummy";
        //    ClientService client = new ClientService();
        //    HttpResponseMessage response = await client.FetchDataFromAPI(url);
        //    Assert.IsTrue(response.IsSuccessStatusCode);
        //}


        [TestMethod]
        public async Task TestAPIReturnsYummyItem()
        {
            var port = 5179;
            var url = $"http://localhost:{port}/api/yummy";
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



        [TestMethod]
        public async Task TestAPIReturnsEveryTimeNewYummyItem()
        {
            ClientService client = new ClientService();
            var port = 5179;
            var url = $"http://localhost:{port}/api/yummy";
            var response = await client.FetchDataFromAPI(url);

            YummyItem? object1;
            if (!response.IsSuccessStatusCode)
                Assert.Fail($"NOT SUCCESSFULL STATUS CODE! \n code: {response.StatusCode}");
            var json = await response.Content.ReadAsStringAsync();
            object1 = JsonConvert.DeserializeObject<YummyItem>(json);
            Assert.IsNotNull(object1);


            response = await client.FetchDataFromAPI(url);
            if (!response.IsSuccessStatusCode)
                Assert.Fail($"NOT SUCCESSFULL STATUS CODE! \n code: {response.StatusCode}");
            YummyItem? object2;
            json = await response.Content.ReadAsStringAsync();
            object2 = JsonConvert.DeserializeObject<YummyItem>(json);
            Assert.IsNotNull(object2);

            //checking
            int attempts = 5;
            int attempt = 0;
            while (attempt < attempts)
            {
                if (IsEqualYummys(object1, object2))
                {
                    response = await client.FetchDataFromAPI(url);
                    if (!response.IsSuccessStatusCode)
                        Assert.Fail($"NOT SUCCESSFULL STATUS CODE! \n code: {response.StatusCode}");
                    json = await response.Content.ReadAsStringAsync();
                    object1 = JsonConvert.DeserializeObject<YummyItem>(json);
                    Assert.IsNotNull(object1);

                    response = await client.FetchDataFromAPI(url);
                    if (!response.IsSuccessStatusCode)
                        Assert.Fail($"NOT SUCCESSFULL STATUS CODE! \n code: {response.StatusCode}");
                    json = await response.Content.ReadAsStringAsync();
                    object2 = JsonConvert.DeserializeObject<YummyItem>(json);
                    Assert.IsNotNull(object2);

                    attempt++;

                    if (attempt == attempts)
                    {
                        Debug.WriteLine($"\nid: {object1.Id}\t\t\tid: {object2.Id}" +
                        $"\n{object1.Name}\t\t{object2.Name}" +
                        $"\n{object1.Cost}\t\t{object2.Cost}" +
                        $"\n{object1.DropChance}\t\t{object2.DropChance}");
                        Assert.Fail($"Objects where the same;\nmax attempts = {attempts}\ndone = {attempt}");
                    }
                }
                else { break; }
            }
            Debug.WriteLine($"\nid: {object1.Id}\t\t\tid: {object2.Id}" +
                $"\n{object1.Name}\t\t{object2.Name}" +
                $"\n{object1.Cost}\t\t{object2.Cost}" +
                $"\n{object1.DropChance}\t\t{object2.DropChance}");
        }

        private bool IsEqualYummys(YummyItem object1, YummyItem object2)
        {
            return String.Equals(object1.Id, object2.Id)
                    &&
                    String.Equals(object1.Name, object2.Name)
                    &&
                    String.Equals(object1.Cost, object2.Cost)
                    &&
                    String.Equals(object1.DropChance, object2.DropChance);
        }









    }
}