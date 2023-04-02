using YummyDrop_online_store.Models;
using YummyDrop_online_store.Pages;
using System;
using YummyDrop_online_store.Services.RandomizeService;

namespace YummyTesting
{
    [TestClass]
    public class TestServices
    {        
        [TestMethod]
        public void TestGetRandomYummyItem()
        {
            var items = GenerateYummyItemsList(5);
            var service = new RandomizeSerivce();
            service.GetRandomYummyItem(items);
            Assert.IsNotNull(items);
        }


        private List<YummyItem> GenerateYummyItemsList(int q)
        {
            var items = new List<YummyItem>();
            for(int i = 0; i< q; i++)
            {
                items.Add(new YummyItem()
                {
                    Id = i,
                    Name = $"Yummy_i",
                    DropChance = 100 * (1 / (Math.Log(i + q))),
                    Cost = i
                }) ;
            }
            return items;
        }

    }
}