using Library.Context;
using Library.Model.Entities;
using Library.Utils;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository;

public class CategoryRepository(AppDbContext dbContext) : ICategoryRepository
{
    public async Task<List<Category>> GetCategoryByNameAsync(string name)
    {
        string cleanName = Escape.EscapeLike(name);
        
        List<Category> res = await (from category in dbContext.Categories.AsNoTracking()
            where EF.Functions.Like(category.Name, cleanName)
            select category).ToListAsync();

        return res;
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        return await dbContext.Categories.AsNoTracking().ToListAsync();
    }

    public async Task<Category> CreateCategoryAsync(Category author)
    {
        dbContext.Categories.Add(author);
        await dbContext.SaveChangesAsync();
        return author;
    }
    
    public async Task<bool> UpdateCategoryAsync(Category author)
    {
        dbContext.Categories.Update(author);
        await dbContext.SaveChangesAsync();
        return true;
    }
    
    public async Task<Category?> GetCategoryByIdAsync(int id)
    {
        return await dbContext.Categories.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
    }

   
}

