using Library.Model.DTO;
using Library.Model.Entities;
using Library.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;


[ApiController]
[Route("api/authors")]
public class AuthorController(IAuthorService authorService) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = "InternalUser")]
    public async Task<IActionResult> Create(CreateAuthor model)
    {
        Author author = await authorService.CreateAuthorAsync(model);
        ReadAuthor readAuthor = ReadAuthor.FromAuthor(author);
        
        return CreatedAtAction(nameof(Get), new { id = readAuthor.Id }, readAuthor);
    }


    [HttpGet]
    [Authorize(Roles = "InternalUser,customer")]
    public async Task<IActionResult> Get()
    {
        List<Author> authors = await authorService.GetAuthorsAsync();
        List<ReadAuthor> readAuthors = ReadAuthor.FromAuthors(authors);
        return Ok(readAuthors);
    }
    
    
    [HttpPut("{id:int}")]
    [Authorize(Roles = "InternalUser")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateAuthor model)
    {
        _ = await authorService.UpdateAuthorAsync(id, model);
        return Ok();
    }
    
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "InternalUser")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        _ = await authorService.DeleteAuthorAsync(id);
        return Ok();
    }
    
    [HttpGet("{id:int}")]
    [Authorize(Roles = "InternalUser,customer")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        Author? author = await authorService.GetAuthorByIdAsync(id);

        if (author is null) return BadRequest();
        ReadAuthor readAuthor = ReadAuthor.FromAuthor(author);
        return Ok(readAuthor);
    }
    
}