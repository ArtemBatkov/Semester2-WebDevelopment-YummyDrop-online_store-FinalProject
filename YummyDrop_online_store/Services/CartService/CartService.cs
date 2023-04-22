using YummySharedLibrary;

namespace YummyDrop_online_store.Services.CartService
{
    public class CartService : ICartService
    {
        private Cart _cart = new Cart();

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
            _cart.addToCart(yummyItem);
            UpdateCartItemCount(_cart.getLengthCart());
        }

        public void RemoveFromCart(YummyItem yummyItem)
        {
            if (yummyItem == null) { return; }
            _cart.removeFromCart(yummyItem);
            UpdateCartItemCount(_cart.getLengthCart());
        }

        public List<YummyItem> GetAllCartObjects()
        {
            return _cart.GetAllCartObjects();
        }
    }
}
