using YummyDrop_online_store.Models;

namespace YummyDrop_online_store.Services.GeneratorService
{
    public interface IGeneratorService
    {
        /// <summary>
        /// Method generates a list of YummyItems
        /// </summary>
        /// <param name="q">quantity of items, default=100</param>
        /// <returns>List of YummyItems</returns>
        public List<YummyItem> GenerateYummyItemsList(int q = 100);
    }
}
