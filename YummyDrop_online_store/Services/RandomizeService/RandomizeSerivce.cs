using YummyDrop_online_store.Models;

namespace YummyDrop_online_store.Services.RandomizeService
{
    public class RandomizeSerivce : IRandomizeService
    {
        public YummyItem GetRandomYummyItem(List<YummyItem> items)
        {
            var quantity = items.Count();
            var random = new Random();
            return items[random.Next(0, quantity)];
        }
    }
}
