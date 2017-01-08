using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BookStore
{
    [DataContract]
    public class Book : IBook
    {
        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "author")]
        public string Author { get; set; }

       [DataMember(Name = "price")]
        public decimal Price { get; set; }
       [DataMember(Name = "inStock")]
        public decimal InStock { get; set; }

        public int ProductId { get; set; }
        public bool CheckInStock { get; set; }
    }


    [DataContract]
    public class RootObject :IEnumerable<IBook>
    {  
        [DataMember(Name = "books")]
        public IEnumerable<Book> Books { get; set; }
        public IEnumerator<IBook> GetEnumerator()
        {
            return Books.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}



