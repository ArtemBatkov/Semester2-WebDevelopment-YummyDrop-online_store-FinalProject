using YummyDrop_online_store.Models;

namespace YummyDrop_online_store.Services.RandomizeService
{
    public class RandomizeSerivce : IRandomizeService
    {
        private static Random random = new Random();
        public int GetRandomId(List<int> ids, bool IsPsevdo = false)
        {
            var quantity = ids.Count();  
            if (IsPsevdo)
            {
                var seedRand = new Random(1);
                return ids[seedRand.Next(0, quantity)];
            }
            var value = random.Next(0, quantity);
            return ids[value];
        }


        public int GetRandomId(int[] ids, bool IsPsevdo = false)
        {
            var quantity = ids.Count();
            if (IsPsevdo)
            {
                var seedRand = new Random(1);
                return ids[seedRand.Next(0, quantity)];
            }
            var value = random.Next(0, quantity);
            return ids[value];
        }
    }
}
