using Library.Exceptions;
using Library.Model.DTO;
using Library.Model.Entities;
using Library.Repository;

namespace Library.Services;

public class BookService(IBookRepository bookRepository, IAuthorRepository authorRepository) : IBookService
{
    public async Task<List<Book>> GetBookByNameAsync(string name)
    {
        return await bookRepository.GetBookByNameAsync(name);
    }

    public async Task<List<Book>> GetBooksAsync()
    {
        return await bookRepository.GetBooksAsync();
    }

    public async Task<Book> CreateBookAsync(CreateBook createBook)
    {
        Book book = createBook.ToEntity();
        
        return await bookRepository.CreateBookAsync(book);      
    }

    public async Task<bool> UpdateBookAsync(int id, UpdateBook book)
    {
        Book? bookExist = await bookRepository.GetBookByIdAsync(id);
        
        if(bookExist is null)
            throw new BookException("Book not found");
        
        bookExist.Update(book.Title, book.ISBN, book.PublishedYear, book.CategoryId);
        
        return await bookRepository.UpdateBookAsync(bookExist);
    }

    public async Task<bool> UpdateBookAuthorsAsync(int id, UpdateBookAuthors bookAuthors)
    {
        Book? bookExist = await bookRepository.GetBookByIdAsync(id);
        
        if(bookExist is null)
            throw new BookException("Book not found");
        
        List<Author> authors = await authorRepository.GetAuthorsByIdsAsync(bookAuthors.AuthorIds);
        
        if(authors.Count != bookAuthors.AuthorIds.Count)
            throw new BookException("One or more authors not found");
        
        bookExist.SetAuthors(authors);
        
        return await bookRepository.UpdateBookAsync(bookExist);
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        Book? authorExist = await bookRepository.GetBookByIdAsync(id);
        
        if(authorExist is null)
            throw new BookException("Book not found");
        
        authorExist.SetAsDeleted();
        
        return await bookRepository.UpdateBookAsync(authorExist);
    }

    public async Task<Book?> GetBookByIdAsync(int id)
    {
        Book? author = await bookRepository.GetBookByIdWithCategoryAuthorsCopiesAsync(id);
        
        return author ?? throw new BookException("Book not found");
    }

    // Copies
    public async Task<List<BookCopy>> GetCopiesByBookIdAsync(int bookId)
    {
        return await bookRepository.GetCopiesByBookIdAsync(bookId);
    }

    public async Task<BookCopy> AddCopyAsync(int bookId, string barcode)
    {
        // ensure book exists
        var book = await bookRepository.GetBookByIdWithCategoryAuthorsCopiesAsync(bookId);
        if (book is null) throw new BookException("Book not found");

        var copy = new BookCopy(bookId, barcode);
        return await bookRepository.AddCopyAsync(copy);
    }

    public async Task<bool> RemoveCopyAsync(int copyId)
    {
        BookCopy? copy = await bookRepository.GetCopyByIdAsync(copyId);
        
        if(copy is null) throw new BookException("Copie not found");
        
        copy.SetAsDeleted();
        
        return await bookRepository.UpdateCopyAsync(copy);
    }

    public async Task<int> CountAvailableCopiesAsync(int bookId)
    {
        return await bookRepository.CountAvailableCopiesAsync(bookId);
    }

    public async Task<BookCopy?> GetAvailableCopyAsync(int bookId)
    {
        return await bookRepository.GetAvailableCopyAsync(bookId);
    }

    public async Task<bool> MarkCopyAsLentAsync(int copyId)
    {
        BookCopy? copy = await bookRepository.GetCopyByIdAsync(copyId);
        
        if(copy is null) throw new BookException("Copy not found");
        if(copy.IsAvailable == false) throw new BookException("Copy is not available");
        
        copy.MarkAsLent();
        
        return await bookRepository.UpdateCopyAsync(copy);
    }

    public async Task<bool> MarkCopyAsReturnedAsync(int copyId)
    {
        BookCopy? copy = await bookRepository.GetCopyByIdAsync(copyId);
        if(copy is null) throw new BookException("Copy not found");
        
        copy.MarkAsReturned();
        return await bookRepository.UpdateCopyAsync(copy);
    }
}