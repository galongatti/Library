using Library.Model.DTO;
using Library.Model.Entities;

namespace Library.Services;

public interface IBookService
{
    Task<List<Book>> GetBookByNameAsync(string name);
    Task<List<Book>> GetBooksAsync();
    Task<Book> CreateBookAsync(CreateBook category);
    Task<bool> UpdateBookAsync(int id, UpdateBook category);
    Task<bool> UpdateBookAuthorsAsync(int id, UpdateBookAuthors bookAuthors);
    Task<bool> DeleteBookAsync(int id);
    Task<Book?> GetBookByIdAsync(int id);
}