using Library.Context;
using Library.Model.Entities;
using Library.Utils;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository;

public class BookRepository(AppDbContext dbContext) : IBookRepository
{
    public async Task<List<Book>> GetBookByName(string name)
    {
        string cleanName = Escape.EscapeLike(name);
        
        List<Book> res = await (from author in dbContext.Books.AsNoTracking()
            where EF.Functions.Like(author.Title, cleanName)
            select author).ToListAsync();

        return res;
    }

    public async Task<List<Book>> GetBooks()
    {
        return await dbContext.Books.Include(c => c.Category).Include(a => a.Authors).AsNoTracking().ToListAsync();
    }

    public async Task<Book> CreateBook(Book author)
    {
        dbContext.Books.Add(author);
        await dbContext.SaveChangesAsync();
        return author;
    }
    
    public async Task<Book?> GetBookByIdTracking(int id)
    {
        return await dbContext.Books.FindAsync(id);
    }

    public async Task<bool> UpdateBook(Book author)
    {
        await dbContext.SaveChangesAsync();
        return true;
    }
    
    public async Task<Book?> GetBookByIdNoTracking(int id)
    {
        return await dbContext.Books.Include(c => c.Category).Include(a => a.Authors).AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
    }
   
}

