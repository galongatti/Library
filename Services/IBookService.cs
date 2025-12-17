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

    // Copies
    Task<List<BookCopy>> GetCopiesByBookIdAsync(int bookId);
    Task<BookCopy> AddCopyAsync(int bookId, string barcode);
    Task<bool> RemoveCopyAsync(int copyId);
    Task<int> CountAvailableCopiesAsync(int bookId);

    // Helpers for lending flow
    Task<BookCopy?> GetAvailableCopyAsync(int bookId);
    Task<bool> MarkCopyAsLentAsync(int copyId);
    Task<bool> MarkCopyAsReturnedAsync(int copyId);
}