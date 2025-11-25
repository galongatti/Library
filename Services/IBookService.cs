using Library.Model.DTO;
using Library.Model.Entities;

namespace Library.Services;

public interface IBookService
{
    Task<List<Book>> GetBookByName(string name);
    Task<List<Book>> GetBooks();
    Task<Book> CreateBook(CreateBook category);
    Task<bool> UpdateBook(int id, UpdateBook category);
    Task<bool> UpdateBookAuthors(int id, UpdateBookAuthors bookAuthors);
    Task<bool> DeleteBook(int id);
    Task<Book?> GetBookByIdNoTracking(int id);
}