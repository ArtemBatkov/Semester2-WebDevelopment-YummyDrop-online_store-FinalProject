using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YummySharedLibrary
{
    public  class Cart
    {
        public int Id { get; set; }

        private  List<YummyItem> CartItems = new List<YummyItem>();
        public  void addToCart(YummyItem item)
        {
            CartItems.Insert(0,item);
           
        }

        public  int getLengthCart()
        {
            return CartItems.Count;
        }


         public  List<YummyItem> GetAllCartObjects()
        {
            return CartItems; 
        }
    }
}
