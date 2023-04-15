using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YummySharedLibrary
{
    public static class Cart
    {
        private static List<YummyItem> CartItems = new List<YummyItem>();
        public static void addToCart(YummyItem item)
        {
            CartItems.Insert(0,item);
           
        }

        public static int getLengthCart()
        {
            return CartItems.Count;
        }


         
    }
}
