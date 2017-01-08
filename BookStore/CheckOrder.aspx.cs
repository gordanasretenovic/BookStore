using System;
using System.Drawing;
using BookStore.Models;

namespace BookStore
{
    public partial class CheckOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Checkout();
        }

        /// <summary>
        /// Metoden som skapar Order.
        /// Går genom kunvagnen och kollar upp
        /// om böcker finns i tillräckligt antal 
        /// eller om de alls finns på lager.
        /// Beroende på, om det finns sådana tar bort eller minskar de 
        /// från kundvagnen, uppdaterar rad värde och
        /// total ordervärde. Skappar meddelande till
        /// användare om ändringar i kundvagnen.
        /// </summary>
        private void Checkout()
        {
            lblTotal.Attributes.Add("Style", "float: right");
            CartItem itemtodelete = null;
            var message = "";
            var cart = Session["Cart"] as Cart;
            if (cart == null)
            {
                lblTotal.Visible = false;
                return;
            }
            foreach (var item in cart.CartItems)
            {
                if (item.Book.CheckInStock)
                {
                    if (item.Book.InStock >= item.Quantity) continue;
                    item.Amount = item.Book.Price * item.Book.InStock;
                    cart.TotalAmount -= item.Book.Price * (item.Quantity - item.Book.InStock);
                    item.Quantity = (int) item.Book.InStock;
                    message += Environment.NewLine + "There is only " + item.Book.InStock + " product '" + item.Book.Title +
                               "' in stock.";
                }
                else
                {
                    itemtodelete = item;
                    cart.TotalAmount -= item.Book.Price * item.Quantity;
                    message += Environment.NewLine + "The product '" + item.Book.Title + "' is out of stock.";
                }
            }
            if (itemtodelete != null)
                cart.CartItems.Remove(itemtodelete);
            Session["Cart"] = cart;
            OrderDetails.DataSource = cart.CartItems;
            OrderDetails.DataBind();
            lblMessage.Text = message;
            lblMessage.Font.Bold = true;
            lblMessage.ForeColor = lblTotal.ForeColor = Color.Red;
            lblTotal.Text += " " + cart.TotalAmount + " kr";
        }
    }
}