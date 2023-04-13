using YummyDrop_online_store.Models;
using MoreLinq.Extensions;

namespace YummyDrop_online_store.Services.GeneratorService
{
    public class GeneratorService : IGeneratorService
    {

        public List<int> GenerateMillionIds(List<YummyItem> yummys)
        {
            int size = 1_000_000;
            List<int> Ids = new List<int>();

            //take the id with the biggest drop chance
            int IdWithMaxDropChance = yummys.OrderByDescending(x => x.DropChance).First().Id; 
            double totalChance = yummys.Sum(x => x.DropChance);

            //fill out the array
            foreach (var yummy in yummys)
            {
                int repeates = (int) Math.Floor(yummy.DropChance / totalChance * size);
                for (int i = 0; i < repeates; i++)
                {
                    Ids.Add(yummy.Id);
                }
            }

            while (Ids.Count < 1_000_000)
            {
                Ids.Add(IdWithMaxDropChance);
            }

            Ids = Ids.Shuffle().ToList();
            return Ids;
        }
    }
}
