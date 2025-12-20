using Library.Model.Entities;

namespace Library.Repository;

public interface IBookRepository
{
    Task<List<Book>> GetBookByNameAsync(string name);
    Task<List<Book>> GetBooksAsync();
    Task<Book> CreateBookAsync(Book category);
    Task<bool> UpdateBookAsync(Book book);
    Task<Book?> GetBookByIdWithCategoryAuthorsCopiesAsync(int id);
    Task<Book?> GetBookByIdAsync(int id);

    // Copies related
    Task<List<BookCopy>> GetCopiesByBookIdAsync(int bookId);
    Task<BookCopy> AddCopyAsync(BookCopy copy);
    Task<bool> UpdateCopyAsync(BookCopy bookCopy);
    Task<int> CountAvailableCopiesAsync(int bookId);
    Task<BookCopy?> GetCopyByIdAsync(int copyId);

    // Helpers for lending flow
    Task<BookCopy?> GetAvailableCopyAsync(int bookId);
  
}