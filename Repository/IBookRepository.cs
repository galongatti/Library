using Library.Model.Entities;

namespace Library.Repository;

public interface IBookRepository
{
    Task<List<Book>> GetBookByName(string name);
    Task<List<Book>> GetBooks();
    Task<Book> CreateBook(Book category);
    Task<bool> UpdateBook(Book category);
    Task<Book?> GetBookByIdTracking(int id);
    Task<Book?> GetBookByIdNoTracking(int id);
}