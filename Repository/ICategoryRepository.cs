using Library.Model.Entities;

namespace Library.Repository;

public interface ICategoryRepository
{
    Task<List<Category>> GetCategoryByName(string name);
    Task<List<Category>> GetCategories();
    Task<Category> CreateCategory(Category category);
    Task<bool> UpdateCategory(Category category);
    Task<Category?> GetCategoryByIdTracking(int id);
    Task<Category?> GetCategoryByIdNoTracking(int id);
}