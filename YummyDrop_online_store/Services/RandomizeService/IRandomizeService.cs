using YummyDrop_online_store.Models;

namespace YummyDrop_online_store.Services.RandomizeService
{
    /// <summary>
    /// IRandomizeService is the service that helps you to make a random pick in the list.
    /// </summary>
    /// 
    public interface IRandomizeService
    {
        /// <summary>
        /// Function selects the random item in the list
        /// </summary>
        /// <param name="items">List of YummyItem objects</param>
        /// <returns>Random YummyItem object</returns>
        public YummyItem GetRandomYummyItem(List<YummyItem> items);
    }
}
