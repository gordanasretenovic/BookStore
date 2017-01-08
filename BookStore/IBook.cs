namespace BookStore
{
   
    public interface IBook
    {       
        string Title { get; }        
        string Author { get; }       
        decimal Price { get; }      
        decimal InStock { get; }
        int ProductId { get; set; }
        bool CheckInStock { get; set; }
    }
}
