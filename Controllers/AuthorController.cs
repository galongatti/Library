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
        Author author = await authorService.CreateAuthorAsync(model);
        return Ok(author);
    }


    [HttpGet]
    public async Task<IActionResult> Get()
    {
        List<Author> authors = await authorService.GetAuthorsAsync();
        List<ReadAuthor> readAuthors = ReadAuthor.FromAuthors(authors);
        return Ok(readAuthors);
    }
    
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateAuthor model)
    {
        bool ok = await authorService.UpdateAuthorAsync(id, model);
        
        if(ok) return Ok();
        
        return BadRequest();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        bool ok = await authorService.DeleteAuthorAsync(id);
        
        if(ok) return Ok();
        
        return BadRequest();
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        Author? author = await authorService.GetAuthorByIdAsync(id);

        if (author is null) return BadRequest();
        ReadAuthor readAuthor = ReadAuthor.FromAuthor(author);
        return Ok(readAuthor);
    }
    
}