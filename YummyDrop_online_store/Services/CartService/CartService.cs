using YummySharedLibrary;

namespace YummyDrop_online_store.Services.CartService
{
    public class CartService : ICartService
    {
        private int _cartItemCount = 0;
        public int CartItemCount => _cartItemCount;

        public event EventHandler CartUpdated;

        public void UpdateCartItemCount(int count)
        {
            _cartItemCount = count;
            CartUpdated?.Invoke(this, EventArgs.Empty);
        }

        public void addToCart(YummyItem yummyItem)
        {
            if(yummyItem == null) { return; }
            Cart.addToCart(yummyItem);
            UpdateCartItemCount(Cart.getLengthCart());
        }
    }
}
