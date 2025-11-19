using Library.Model.DTO;
using Library.Model.Entities;
using Library.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;


[ApiController]
[Route("api/authors")]
public class AuthorController(IAuthorService authorService) : ControllerBase
{
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateAuthor model)
    {
        Author author = await authorService.CreateAuthor(model);
        return Ok(author);
    }


    [HttpGet]
    public async Task<IActionResult> Get()
    {
        List<Author> authors = await authorService.GetAuthors();
        return Ok(authors);
    }
    
    
    [HttpPut("/{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateAuthor model)
    {
        bool sucess = await authorService.UpdateAuthor(id, model);
        
        if(sucess) return Ok();
        
        return BadRequest();
    }
    
    [HttpDelete("/{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        bool sucess = await authorService.DeleteAuthor(id);
        
        if(sucess) return Ok();
        
        return BadRequest();
    }
    
    [HttpGet("/{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        Author? author = await authorService.GetAuthorByIdNoTracking(id);
        
        if(author is not null) return Ok(author);
        
        return BadRequest();
    }
    
    
    
}