using Library.Context;
using Library.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository;

public class AuthorRepository(AppDbContext dbContext) : IAuthorRepository
{
    public async Task<List<Author>> GetAuthorByName(string name)
    {
        List<Author> res = await (from author in dbContext.Authors.AsNoTracking()
            where EF.Functions.Like(author.Name, $"%{name}%")
            select author).ToListAsync();

        return res;
    }

    public async Task<List<Author>> GetAuthors()
    {
        return await dbContext.Authors.AsNoTracking().ToListAsync();
    }

    public async Task<Author> CreateAuthor(Author author)
    {
        dbContext.Authors.Add(author);
        await dbContext.SaveChangesAsync();
        return author;
    }
    
    public async Task<Author?> GetAuthorByIdTracking(int id)
    {
        return await dbContext.Authors.FindAsync(id);
    }

    public async Task<bool> UpdateAuthor(Author author)
    {
        await dbContext.SaveChangesAsync();
        return true;
    }
    
    public async Task<Author?> GetAuthorByIdNoTracking(int id)
    {
        return await dbContext.Authors.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
    }

   
}

