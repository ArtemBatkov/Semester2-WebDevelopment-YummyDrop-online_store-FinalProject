using YummyDrop_online_store.Services.GeneratorService;
using YummyDrop_online_store.Services.ClientService;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Extensions;
using YummyDrop_online_store.Controllers;
using YummyDrop_online_store.Services.RandomizeService;

namespace YummyTesting
{
    [TestClass]
    public class TestServices
    {
        private readonly YummyAPIController _contrl;

        public TestServices(YummyAPIController contrl)
        {
            var services = new ServiceCollection();          
            services.AddScoped<YummyAPIController>();

            var serviceProvider = services.BuildServiceProvider();
            _contrl = serviceProvider.GetRequiredService<YummyAPIController>();
        }
        


 
        
        [TestMethod]
        public async Task TestAPIGetRandromYummyItemStatusCode()
        {
            var service = new GeneratorService();
            var random = new Random();
            var items = service.GenerateYummyItemsList();
            var url = "http://localhost:5179/api/yummy";
            ClientService client = new ClientService();
            HttpResponseMessage response = await client.FetchDataFromAPI(url);
            Assert.IsTrue(response.IsSuccessStatusCode);
        }
        



        [TestMethod]
        public async  Task TestAPIGetRandomYummyItem()
        {
            ClientService client = new ClientService();
            var port = 5179;

            var service = new GeneratorService();
            var random = new Random();
            var items = service.GenerateYummyItemsList();
            var url = $"http://localhost:{port}/api/yummy";         
            
            HttpResponseMessage response = await client.FetchDataFromAPI(url);            
             

            string json = await response.Content.ReadAsStringAsync();
            var converter = JsonConvert.DeserializeObject<dynamic>(json);
            int id = converter.id;
            string name = converter.name;
            double chance = converter.dropChance;
            decimal cost = converter.cost;

            Assert.IsNotNull(name);
            Assert.IsNotNull(name);
            Assert.IsNotNull(chance);
            Assert.IsNotNull(cost);
        }

       
    }
}