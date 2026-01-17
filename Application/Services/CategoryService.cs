using Library.Exceptions;
using Library.Model.DTO;
using Library.Model.Entities;
using Library.Repository;

namespace Library.Services;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    public async Task<List<Category>> GetCategoryByNameAsync(string name)
    {
        return await categoryRepository.GetCategoryByNameAsync(name);
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        return await categoryRepository.GetCategoriesAsync();
    }

    public async Task<Category> CreateCategoryAsync(CreateCategory newCategory)
    {
        Category category = newCategory.ToEntity();
        return await categoryRepository.CreateCategoryAsync(category);
    }

    public async Task<bool> UpdateCategoryAsync(int id, UpdateCategory updateCategory)
    {
        Category? category = await categoryRepository.GetCategoryByIdAsync(id);
        
        if(category is null)
            throw new CategoryException("Category not found");
        
        category.Update(updateCategory.Name);
        return await categoryRepository.UpdateCategoryAsync(category);
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        Category? category = await categoryRepository.GetCategoryByIdAsync(id);
        
        if(category is null)
            throw new CategoryException("Category not found");
        
        category.SetAsDeleted();
        return await categoryRepository.UpdateCategoryAsync(category);
    }

    public async Task<Category?> GetCategoryByIdAsync(int id)
    {
        return await categoryRepository.GetCategoryByIdAsync(id);
    }
}