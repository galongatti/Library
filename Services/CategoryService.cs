using Library.Exceptions;
using Library.Model.DTO;
using Library.Model.Entities;
using Library.Repository;

namespace Library.Services;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    public async Task<List<Category>> GetCategoryByName(string name)
    {
        return await categoryRepository.GetCategoryByName(name);
    }

    public async Task<List<Category>> GetCategories()
    {
        return await categoryRepository.GetCategories();
    }

    public async Task<Category> CreateCategory(CreateCategory newCategory)
    {
        Category category = newCategory.ToEntity();
        return await categoryRepository.CreateCategory(category);
    }

    public async Task<bool> UpdateCategory(int id, UpdateCategory updateCategory)
    {
        Category? category = await categoryRepository.GetCategoryByIdTracking(id);
        
        if(category is null)
            throw new CategoryException("Category not found");
        
        category.Update(updateCategory.Name);
        return await categoryRepository.UpdateCategory(category);
    }

    public async Task<bool> DeleteCategory(int id)
    {
        Category? category = await categoryRepository.GetCategoryByIdTracking(id);
        
        if(category is null)
            throw new CategoryException("Category not found");
        
        category.SetAsDeleted();
        return await categoryRepository.UpdateCategory(category);
    }

    public async Task<Category?> GetCategoryByIdNoTracking(int id)
    {
        return await categoryRepository.GetCategoryByIdNoTracking(id);
    }
}