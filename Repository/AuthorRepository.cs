using Library.Context;
using Library.Model.Entities;
using Library.Utils;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository;

public class AuthorRepository(AppDbContext dbContext) : IAuthorRepository
{
    public async Task<List<Author>> GetAuthorByNameAsync(string name)
    {
        string cleanName = Escape.EscapeLike(name);
        
        List<Author> res = await (from author in dbContext.Authors.AsNoTracking()
            where EF.Functions.Like(author.Name, cleanName)
            select author).ToListAsync();

        return res;
    }

    public async Task<List<Author>> GetAuthorsAsync()
    {
        return await dbContext.Authors.AsNoTracking().ToListAsync();
    }

    public async Task<Author> CreateAuthorAsync(Author author)
    {
        dbContext.Authors.Add(author);
        await dbContext.SaveChangesAsync();
        return author;
    }
    

    public async Task<bool> UpdateAuthorAsync(Author author)
    {
        dbContext.Authors.Update(author);
        await dbContext.SaveChangesAsync();
        return true;
    }
    
    public async Task<Author?> GetAuthorByIdAsync(int id)
    {
        return await dbContext.Authors.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<List<Author>> GetAuthorsByIdsAsync(List<int> ids)
    {
        return await dbContext.Authors.AsNoTracking()
            .Where(a => ids.Contains(a.Id))
            .ToListAsync();
    }
}

