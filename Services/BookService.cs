using Library.Exceptions;
using Library.Model.DTO;
using Library.Model.Entities;
using Library.Repository;

namespace Library.Services;

public class BookService(IBookRepository bookRepository, IAuthorRepository authorRepository) : IBookService
{
    public Task<List<Book>> GetBookByName(string name)
    {
        return bookRepository.GetBookByName(name);
    }

    public async Task<List<Book>> GetBooks()
    {
        return await bookRepository.GetBooks();
    }

    public async Task<Book> CreateBook(CreateBook createBook)
    {
        Book book = createBook.ToEntity();
        
        return await bookRepository.CreateBook(book);      
    }

    public async Task<bool> UpdateBook(int id, UpdateBook book)
    {

        Book? bookExist = await bookRepository.GetBookByIdTracking(id);
        
        if( bookExist is null)
            throw new BookException("Book not found");
        
        bookExist.Update(book.Title, book.ISBN, book.PublishedYear, book.CategoryId);
        
        return await bookRepository.UpdateBook(bookExist);
    }

    public async Task<bool> UpdateBookAuthors(int id, UpdateBookAuthors bookAuthors)
    {
        Book? bookExist = await bookRepository.GetBookByIdTracking(id);
        
        if( bookExist is null)
            throw new BookException("Book not found");
        
        List<Author> authors = await authorRepository.GetAuthors();
        
        List<Author> bookAuthorsToAdd = authors.Where(a => bookAuthors.AuthorIds.Contains(a.Id)).ToList();
        
        bookExist.SetAuthors(bookAuthorsToAdd);
        
        return await bookRepository.UpdateBook(bookExist);
    }

    public async Task<bool> DeleteBook(int id)
    {
        Book? authorExist = await bookRepository.GetBookByIdTracking(id);
        
        if( authorExist is null)
            throw new BookException("Book not found");
        
        authorExist.SetAsDeleted();
        
        return await bookRepository.UpdateBook(authorExist);
    }

    public async Task<Book?> GetBookByIdNoTracking(int id)
    {
        Book? author = await bookRepository.GetBookByIdNoTracking(id);
        
        return author ?? throw new BookException("Book not found");
    }
}