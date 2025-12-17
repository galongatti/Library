using Library.Model.Entities;

namespace Library.Repository;

public interface IBookRepository
{
    Task<List<Book>> GetBookByNameAsync(string name);
    Task<List<Book>> GetBooksAsync();
    Task<Book> CreateBookAsync(Book category);
    Task<bool> UpdateBookAsync(Book category);
    Task<Book?> GetBookByIdAsync(int id);

    // Copies related
    Task<List<BookCopy>> GetCopiesByBookIdAsync(int bookId);
    Task<BookCopy> AddCopyAsync(BookCopy copy);
    Task<bool> RemoveCopyAsync(int copyId);
    Task<int> CountAvailableCopiesAsync(int bookId);

    // Helpers for lending flow
    Task<BookCopy?> GetAvailableCopyAsync(int bookId);
    Task<bool> MarkCopyAsLentAsync(int copyId);
    Task<bool> MarkCopyAsReturnedAsync(int copyId);
}