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
        Book? author = await bookRepository.GetBookByIdAsync(id);
        
        return author ?? throw new BookException("Book not found");
    }
}