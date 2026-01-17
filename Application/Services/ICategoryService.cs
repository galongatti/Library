using Library.Model.DTO;
using Library.Model.Entities;

namespace Library.Services;

public interface ICategoryService
{
    Task<List<Category>> GetCategoryByNameAsync(string name);
    Task<List<Category>> GetCategoriesAsync();
    Task<Category> CreateCategoryAsync(CreateCategory category);
    Task<bool> UpdateCategoryAsync(int id, UpdateCategory category);
    Task<bool> DeleteCategoryAsync(int id);
    Task<Category?> GetCategoryByIdAsync(int id);
}