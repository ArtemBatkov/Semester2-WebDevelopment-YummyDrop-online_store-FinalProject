using YummyDrop_online_store.Models;

namespace YummyDrop_online_store.Services.GeneratorService
{
    public class GeneratorService: IGeneratorService
    {
        public List<YummyItem> GenerateYummyItemsList(int q = 100)
        {
            var items = new List<YummyItem>(q);
            var a = 10;
            for (int i = 0; i < q; i++)
            {
                double p = 1 / (1 + Math.Exp((q - i) / a)) * 100;
                items.Add(new YummyItem()
                {
                    Id = i,
                    Name = $"Yummy_{i}",
                    DropChance = p,
                    Cost = i
                });;
            }
            return items;
        }
    }
}
