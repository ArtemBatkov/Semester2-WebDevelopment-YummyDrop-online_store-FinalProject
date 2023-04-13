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
        /// Method return a random id
        /// </summary>
        /// <param name="ids">List of ids</param>
        /// <param name="IsPsevdo">Predicted value? False is default</param>
        /// <returns>A random id</returns>
        public int GetRandomId(List<int> ids, bool IsPsevdo = false);
    }
}
