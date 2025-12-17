using Library.Context;
using Library.Model.Entities;
using Library.Utils;
using Microsoft.EntityFrameworkCore;

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
        return await dbContext.Books.Include(c => c.Category).Include(a => a.Authors).AsNoTracking().ToListAsync();
    }

    public async Task<Book> CreateBookAsync(Book author)
    {
        dbContext.Books.Add(author);
        await dbContext.SaveChangesAsync();
        return author;
    }
    
    public async Task<bool> UpdateBookAsync(Book author)
    {
        dbContext.Books.Update(author);
        await dbContext.SaveChangesAsync();
        return true;
    }
    
    public async Task<Book?> GetBookByIdAsync(int id)
    {
        return await dbContext.Books.Include(c => c.Category).Include(a => a.Authors).Include(b => b.Copies).AsNoTracking().SingleOrDefaultAsync(a => a.Id == id);
    }
   
    // Copies related
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

    public async Task<bool> RemoveCopyAsync(int copyId)
    {
        var copy = await dbContext.BookCopies.FindAsync(copyId);
        if (copy is null) return false;
        dbContext.BookCopies.Remove(copy);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<int> CountAvailableCopiesAsync(int bookId)
    {
        return await dbContext.BookCopies.CountAsync(c => c.BookId == bookId && c.IsAvailable);
    }

    public async Task<BookCopy?> GetAvailableCopyAsync(int bookId)
    {
        return await dbContext.BookCopies.FirstOrDefaultAsync(c => c.BookId == bookId && c.IsAvailable);
    }

    public async Task<bool> MarkCopyAsLentAsync(int copyId)
    {
        var copy = await dbContext.BookCopies.FindAsync(copyId);
        if (copy is null) return false;
        copy.MarkAsLent();
        dbContext.BookCopies.Update(copy);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> MarkCopyAsReturnedAsync(int copyId)
    {
        var copy = await dbContext.BookCopies.FindAsync(copyId);
        if (copy is null) return false;
        copy.MarkAsReturned();
        dbContext.BookCopies.Update(copy);
        await dbContext.SaveChangesAsync();
        return true;
    }

}
