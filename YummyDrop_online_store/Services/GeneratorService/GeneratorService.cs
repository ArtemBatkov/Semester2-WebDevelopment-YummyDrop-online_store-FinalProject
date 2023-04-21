 
using MoreLinq.Extensions;
using YummySharedLibrary;

namespace YummyDrop_online_store.Services.GeneratorService
{
    public class GeneratorService : IGeneratorService
    {

        public List<int> GenerateMillionIds(List<YummyItem> yummys)
        {
            if(yummys == null) throw(new ArgumentNullException(nameof(yummys)));
            int size = 1_000_000;
            List<int> Ids = new List<int>();
            //take the id with the biggest drop chance
            int IdWithMaxDropChance = yummys.OrderByDescending(x => x.DropChance).First().Id;
            double totalChance = yummys.Sum(x => x.DropChance);

            //fill out the array
            Dictionary<int, int> Repeats = new Dictionary<int, int>();
            foreach (var yummy in yummys)
            {
                int repeates = (int)Math.Floor(yummy.DropChance / totalChance * size);
                Repeats[yummy.Id] = repeates;                
            }

            foreach(var obj in Repeats)
            {
                Ids.AddRange(Enumerable.Repeat(obj.Key, obj.Value).ToList());
            }
            int diff = Math.Abs(size - Ids.Count);
            for (int i = 0; i < diff; i++)
            {
                Ids.Add(IdWithMaxDropChance);
            }           
            
            return Ids.Shuffle().ToList();           
        }


        public int[] GenerateMillionIds(YummyItem [] yummys)
        {
            if (yummys == null) throw (new ArgumentNullException(nameof(yummys)));
            int size = 1_000_000;
            int [] Ids = new int[size];
            
            //take the id with the biggest drop chance
            int IdWithMaxDropChance = yummys.OrderByDescending(x => x.DropChance).First().Id;
            double totalChance = yummys.Sum(x => x.DropChance);

            //fill out the dictionary
            Dictionary<int, int> Repeats = new Dictionary<int, int>();
            foreach (var yummy in yummys)
            {
                int repeates = (int)Math.Floor(yummy.DropChance / totalChance * size);
                Repeats[yummy.Id] = repeates;
            }

            int startIndex = 0;
            foreach (var obj in Repeats)
            {
                Array.Fill(Ids, obj.Key, startIndex, obj.Value);
                startIndex += obj.Value-1;                
            }
            if (Ids.Contains(0))
            {
                Ids = Ids.Select(x => x == 0 ? IdWithMaxDropChance : x).ToArray();
            }             
            Ids.Shuffle();
            return Ids;
        }


         public int[] GenerateMillionMulticast(Dictionary<int, double> multiDic)
        {
            if (multiDic == null) throw (new ArgumentNullException(nameof(multiDic)));
            int size = 1_000_000;
            int[] multis = new int[size];

            int IdWithMaxDropChance = multiDic.OrderByDescending(kv => kv.Value).First().Key;
            double totalChance = multiDic.Sum(x => x.Value);

            int startIndex = 0;
            foreach (var obj in multiDic)
            {
                var count = (int)(obj.Value/totalChance * size);
                Array.Fill(multis, obj.Key, startIndex, count);
                startIndex += count - 1;
            }
            if (multis.Contains(0))
            {
                multis = multis.Select(x => x == 0 ? IdWithMaxDropChance : x).ToArray();
            }
            multis = multis.Shuffle().ToArray();
            return multis;
        }


    }
}
