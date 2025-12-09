using Library.Model.Entities;

namespace Library.Repository;

public interface IBookRepository
{
    Task<List<Book>> GetBookByNameAsync(string name);
    Task<List<Book>> GetBooksAsync();
    Task<Book> CreateBookAsync(Book category);
    Task<bool> UpdateBookAsync(Book category);
    Task<Book?> GetBookByIdAsync(int id);
}