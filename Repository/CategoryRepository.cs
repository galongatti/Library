using Library.Context;
using Library.Model.Entities;
using Library.Utils;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository;

public class CategoryRepository(AppDbContext dbContext) : ICategoryRepository
{
    public async Task<List<Category>> GetCategoryByName(string name)
    {
        string cleanName = Escape.EscapeLike(name);
        
        List<Category> res = await (from category in dbContext.Categories.AsNoTracking()
            where EF.Functions.Like(category.Name, cleanName)
            select category).ToListAsync();

        return res;
    }

    public async Task<List<Category>> GetCategories()
    {
        return await dbContext.Categories.AsNoTracking().ToListAsync();
    }

    public async Task<Category> CreateCategory(Category author)
    {
        dbContext.Categories.Add(author);
        await dbContext.SaveChangesAsync();
        return author;
    }
    
    public async Task<Category?> GetCategoryByIdTracking(int id)
    {
        return await dbContext.Categories.FindAsync(id);
    }

    public async Task<bool> UpdateCategory(Category author)
    {
        await dbContext.SaveChangesAsync();
        return true;
    }
    
    public async Task<Category?> GetCategoryByIdNoTracking(int id)
    {
        return await dbContext.Categories.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
    }

   
}

