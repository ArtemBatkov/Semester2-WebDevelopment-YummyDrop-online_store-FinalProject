using YummySharedLibrary;

namespace YummyDrop_online_store.Services.CartService
{
    public interface ICartService
    {
        int CartItemCount { get; }
        void UpdateCartItemCount(int count);
        event EventHandler CartUpdated;
        public void addToCart(YummyItem yummyItem);
        public List<YummyItem> GetAllCartObjects();
    }
}
