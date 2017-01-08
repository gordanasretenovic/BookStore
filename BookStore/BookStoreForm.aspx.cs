using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using BookStore.Models;
using System.Data.Entity;


namespace BookStore
{
    public partial class BookStoreForm : System.Web.UI.Page, IBookstoreService
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            rbl.SelectedValue = "Alla";
            rootobjectcatalog.DataSource = GetJson();
            rootobjectcatalog.DataBind();
            TextBox1.Attributes["onclick"] = "clearTextBox(this.id)";
        }

        /// <summary>
        /// Hämtar JSon från webben, converterar till
        /// RootObject, lägger till ProductId och flaggan
        /// om produkt finns i lager
        /// </summary>
        /// <returns>IBook list</returns>

        private static IEnumerable<IBook> GetJson()
        {

            var json = new WebClient().DownloadString("http://www.contribe.se/arbetsprov-net/books.json");
            var jsonObjectBookInfo =
                new DataContractJsonSerializer(typeof(RootObject));
            var stream =
                new MemoryStream(Encoding.UTF8.GetBytes(json));
            var books =
                (RootObject) jsonObjectBookInfo.ReadObject(stream);

            int i = 1;

            foreach (var book in books)
            {
                book.ProductId = i;
                book.CheckInStock = book.InStock >= 1;
                i++;
            }
            return books;
        }

        /// <summary>
        /// "Add to cart" button event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected  void CommandBtn_Click(object sender, CommandEventArgs e)
        {
            AddToCart(e);
        }

        /// <summary>
        /// Metoden som lägger till produkt till Kundvagn.
        /// Om Kundvagnen är inte tom, kontrolerar om produkt ligger
        /// redan i kundvagnen.
        /// Om ja plusar antal på produkt, höjer radvärde och ordervärde med produktpris.
        /// Om inte lägger ny produkt i kundvagn, sätter radvärde
        /// och plusar total ordervärde för produktpris.
        /// </summary>
        /// <param name="e"></param>

        private void AddToCart(CommandEventArgs e)
        {
            var id = int.Parse(e.CommandArgument.ToString());
            var books = GetBooksAsync(null);
            var cart = Session["Cart"] as Cart;
            var match = false;
            if (books != null)
                foreach (var book in books.Result)
                {
                    if (match) continue;
                    if (book.ProductId != id) continue;
                    CartItem cartItem;
                    if (cart != null)
                    {
                        foreach (var item in cart.CartItems)
                        {
                            if (item.Book.ProductId.Equals(id))
                            {
                                item.Quantity++;
                                item.Amount += item.Book.Price;
                                cart.TotalAmount += item.Book.Price;
                                match = true;
                                break;
                            }
                        }
                        if (match) continue;
                        cartItem = new CartItem
                        {
                            Book = book,
                            Quantity = 1,
                            Amount = book.Price
                        };

                        cart.CartItems.Add(cartItem);
                        cart.TotalAmount += cartItem.Book.Price;
                    }
                    else
                    {
                        cartItem = new CartItem
                        {
                            Book = book,
                            Quantity = 1,
                            Amount = book.Price
                        };

                        cart = new Cart()
                        {
                            CartItems = new List<CartItem>() {cartItem},
                            TotalAmount = cartItem.Quantity * cartItem.Book.Price
                        };
                        break;
                    }
                }
            Session["Cart"] = cart;
        }
        /// <summary>
        /// Metoden som söker i Konverterade JSon object
        /// och returnerar result
        /// beroende på sök villkor
        /// </summary>
        /// <param name="searchString">TextBox1 text</param>
        /// <returns>Booklistan efter sökuppgift</returns>

        //hittar inte lösning för att köra metotoden async. Instalerrat EF 6.1.3
        //hämtat System.Data.Entity, men GetListAsync() fungerar inte.
        // Därför en annan lösning
        public Task<IEnumerable<IBook>> GetBooksAsync(string searchString)
        {
            var books = GetJson();
            //IEnumerable<IBook> booklist;
            var tcs = new TaskCompletionSource<IEnumerable<IBook>>();
            if (searchString != null)
            {
                switch (rbl.SelectedValue)
                {
                    case "Title":
                        tcs.SetResult(books.Where(b => b.Title.ToLower().StartsWith(searchString.ToLower())));
                        //booklist = await //.ToListAsync();
                        break;
                    case "Author":
                        tcs.SetResult(books.Where(b => b.Author.ToLower().StartsWith(searchString.ToLower())));
                      //booklist = await books.Where(b => b.Author.ToLower().StartsWith(searchString.ToLower())).ToListAsync();
                        break;
                    case "Alla":
                        tcs.SetResult(books);
                        //booklist = books;
                        TextBox1.Text = "";
                        break;
                    default:
                        tcs.SetResult(
                            //booklist = await
                            books.Where(
                                b =>
                                    b.Title.ToLower().StartsWith(searchString.ToLower()) ||
                                    b.Author.ToLower().StartsWith(searchString.ToLower()))); //.ToListAsync();
                        break;
                }
            }
            else
            {
                tcs.SetResult(books);
                //booklist =  books;
            }

            return tcs.Task;
            //return booklist;
        }

        /// <summary>
        /// Anropar GetBooksAsync efter input
        /// som sökvärde.
        /// Därefter anropar metoden SetdataSorce med listan som inparameter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
           
            var books = GetBooksAsync(TextBox1.Text);
           SetdataSource(books);
        }

        /// <summary>
        /// Om radiobutton "Alla" är checked,
        /// hämtar listan på alla produkt och
        /// anropar SetdataSource.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void rbl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbl.SelectedValue == "Alla")
            {
                var books = GetBooksAsync("");
                SetdataSource(books);
            }
        }

        /// <summary>
        /// Metoden som sätter datakällan
        /// till asp:repeater
        /// </summary>
        /// <param name="books">Booklist beroende på sök uppgiften</param>

        private void SetdataSource(Task<IEnumerable<IBook>> books)
        {
            rootobjectcatalog
                .DataSource = books.Result;
            rootobjectcatalog.DataBind();
        }
    }
}
