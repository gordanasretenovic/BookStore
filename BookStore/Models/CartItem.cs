using System.Collections.Generic;

namespace BookStore.Models
{
    public class CartItem 
    {      
        public  IBook Book { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }   
    }

    public class Cart
    {
        public List<CartItem> CartItems { get; set; }
        public decimal TotalAmount { get; set; }
        
    }

}