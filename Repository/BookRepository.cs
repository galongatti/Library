using Library.Context;
using Library.Model.Entities;
using Library.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Library.Repository;

public class BookRepository(AppDbContext dbContext) : IBookRepository
{
    public async Task<List<Book>> GetBookByNameAsync(string name)
    {
        string cleanName = Escape.EscapeLike(name);

        List<Book> res = await (from author in dbContext.Books.AsNoTracking()
            where EF.Functions.Like(author.Title, cleanName)
            select author).ToListAsync();

        return res;
    }

    public async Task<List<Book>> GetBooksAsync()
    {
        return await dbContext.Books.
            Include(c => c.Category).
            Include(a => a.Authors).
            AsNoTracking().ToListAsync();
    }

    public async Task<Book> CreateBookAsync(Book author)
    {
        dbContext.Books.Add(author);
        await dbContext.SaveChangesAsync();
        return author;
    }

    public async Task<bool> UpdateBookAsync(Book book)
    {
        dbContext.Books.Update(book);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<Book?> GetBookByIdWithCategoryAuthorsCopiesAsync(int id)
    {
        return await dbContext.Books.
            Include(c => c.Category).
            Include(a => a.Authors).
            Include(c => c.Copies)
            .AsNoTracking()
            .SingleOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Book?> GetBookByIdAsync(int id)
    {
        return await dbContext.Books.AsNoTracking().SingleOrDefaultAsync(a => a.Id == id);
    }

    public async Task<List<BookCopy>> GetCopiesByBookIdAsync(int bookId)
    {
        return await dbContext.BookCopies.Where(c => c.BookId == bookId).AsNoTracking().ToListAsync();
    }

    public async Task<BookCopy> AddCopyAsync(BookCopy copy)
    {
        dbContext.BookCopies.Add(copy);
        await dbContext.SaveChangesAsync();
        return copy;
    }

    public async Task<bool> UpdateCopyAsync(BookCopy bookCopy)
    {
        dbContext.BookCopies.Update(bookCopy);
        await dbContext.SaveChangesAsync();
        return true;
    }
    
    public async Task<int> CountAvailableCopiesAsync(int bookId)
    {
        return await dbContext.BookCopies.CountAsync(c => c.BookId == bookId && c.IsAvailable);
    }

    public async Task<BookCopy?> GetCopyByIdAsync(int copyId)
    {
        return await dbContext.BookCopies.AsNoTracking().SingleOrDefaultAsync(c => c.Id == copyId);
    }

    public async Task<BookCopy?> GetAvailableCopyAsync(int bookId)
    {
        return await dbContext.BookCopies.AsNoTracking().FirstOrDefaultAsync(c => c.Id == bookId && c.IsAvailable);
    }
}