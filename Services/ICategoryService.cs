using Library.Model.DTO;
using Library.Model.Entities;

namespace Library.Services;

public interface ICategoryService
{
    Task<List<Category>> GetCategoryByName(string name);
    Task<List<Category>> GetCategories();
    Task<Category> CreateCategory(CreateCategory category);
    Task<bool> UpdateCategory(int id, UpdateCategory category);
    Task<bool> DeleteCategory(int id);
    Task<Category?> GetCategoryByIdNoTracking(int id);
}