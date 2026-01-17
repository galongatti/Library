using Library.Model.DTO;
using Library.Model.Entities;
using Library.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "InternalUser,customer")]
    public async Task<ActionResult<List<ReadCategory>>> GetAll()
    {
        List<Category> categories = await categoryService.GetCategoriesAsync();
        return Ok(ReadCategory.FromCategories(categories));
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "InternalUser,customer")]
    public async Task<ActionResult<ReadCategory>> GetById(int id)
    {
        Category? category = await categoryService.GetCategoryByIdAsync(id);
        if (category is null)
            return NotFound();
        
        return Ok(ReadCategory.FromCategory(category));
    }

    [HttpGet("search")]
    [Authorize(Roles = "InternalUser,customer")]
    public async Task<ActionResult<List<ReadCategory>>> GetByName([FromQuery] string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest("Name is required.");
        
        List<Category> categories = await categoryService.GetCategoryByNameAsync(name);
        List<ReadCategory> readCategories = ReadCategory.FromCategories(categories);
        
        return Ok(readCategories);
    }

    [HttpPost]
    [Authorize(Roles = "InternalUser")]
    public async Task<ActionResult<Category>> Create([FromBody] CreateCategory dto)
    {
        Category created = await categoryService.CreateCategoryAsync(dto);
        ReadCategory readCategory = ReadCategory.FromCategory(created);
        
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, readCategory);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "InternalUser")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateCategory dto)
    {
        bool ok = await categoryService.UpdateCategoryAsync(id, dto);
        
        if(ok) return Ok();
        
        return BadRequest();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "InternalUser")]
    public async Task<ActionResult> Delete(int id)
    {
        bool ok = await categoryService.DeleteCategoryAsync(id);
        if(ok) return Ok();
        
        return BadRequest();
    }
}