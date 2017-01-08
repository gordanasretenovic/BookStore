using System;
using System.Drawing;
using System.Globalization;
using System.Web.UI.WebControls;
using BookStore.Models;

namespace BookStore
{
    public partial class ShoppingCart : System.Web.UI.Page
    {       
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            ShowShoppingCart();
            Label1.ForeColor = Color.Red;
            Label1.Font.Bold = true;

        }
        /// <summary>
        /// Visar kundvagn 
        /// </summary>
        public void ShowShoppingCart()
        {
            var cart = Session["Cart"] as Cart;
            if (cart == null) return;
            cartitemcatalog.DataSource = cart.CartItems;
            cartitemcatalog.DataBind();
            Label1.Visible = true;
            lblamount.Visible = true;
            lblamount.Text = cart.TotalAmount.ToString(CultureInfo.InvariantCulture) + " kr";
        }

        /// <summary>
        /// Button tabort från kunvagnen event
        /// Annropar metoden RemoveItemFromCart
        /// och sedan ShowShoppingCart med uppdateringen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnCommand(object sender, CommandEventArgs e)
        {
            RemoveItemFromCart(e);
            ShowShoppingCart();
        }

        /// <summary>
        /// Lopar genom kunvagnen och kollar efter produktantal i den.
        /// Om Antal är större än ett, minskar antal och minskar radvärde och ordervärde för produktpris.
        /// Om Antal är ett tar bort produkten från kundvagnen och minskar ördervärde för produktpris.
        /// </summary>
        /// <param name="e"></param>

        private void RemoveItemFromCart(CommandEventArgs e)
        {
            var cart = Session["Cart"] as Cart;
            var id = int.Parse(e.CommandArgument.ToString());
            if (cart != null)
            {
                for (var i = 0; i < cart.CartItems.Count; i++)
                {
                    if (cart.CartItems[i].Book.ProductId != id) continue;
                    if (cart.CartItems[i].Quantity > 1)
                    {
                        cart.TotalAmount -= cart.CartItems[i].Book.Price;
                        cart.CartItems[i].Quantity--;
                    }
                    else
                    {
                        cart.TotalAmount -= cart.CartItems[i].Book.Price * cart.CartItems[i].Quantity;
                        cart.CartItems.Remove(cart.CartItems[i]);
                    }
                }
            }

            Session["Cart"] = cart;
        }
    }
}