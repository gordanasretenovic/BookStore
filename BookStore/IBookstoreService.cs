using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore
{
    public interface IBookstoreService 
    {
        Task<IEnumerable<IBook>> GetBooksAsync(string searchString);
    }
}
