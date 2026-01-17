using Library.Model.Entities;

namespace Library.Repository;

public interface ICategoryRepository
{
    Task<List<Category>> GetCategoryByNameAsync(string name);
    Task<List<Category>> GetCategoriesAsync();
    Task<Category> CreateCategoryAsync(Category category);
    Task<bool> UpdateCategoryAsync(Category category);
    Task<Category?> GetCategoryByIdAsync(int id);
}