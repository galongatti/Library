using Library.Model.DTO;
using Library.Model.Entities;
using Library.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;


[ApiController]
[Route("api/books")]
public class BookController(IBookService bookService) : ControllerBase
{
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateBook model)
    {
        Book book = await bookService.CreateBookAsync(model);
        return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
    }


    [HttpGet]
    public async Task<IActionResult> Get()
    {
        List<Book> books = await bookService.GetBooksAsync();
        List<ReadBook> readBooks = ReadBook.FromBooks(books);
        return Ok(readBooks);
    }
    
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBook model)
    {
        bool ok = await bookService.UpdateBookAsync(id, model);
        
        if(ok) return Ok();
        
        return BadRequest();
    }
    
    [HttpPut("{id:int}/authors")]
    public async Task<IActionResult> UpdateBookAuthors([FromRoute] int id, [FromBody] UpdateBookAuthors model)
    {
        bool ok = await bookService.UpdateBookAuthorsAsync(id, model);
        
        if(ok) return Ok();
        
        return BadRequest();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        bool ok = await bookService.DeleteBookAsync(id);
        
        if(ok) return Ok();
        
        return BadRequest();
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        Book? book = await bookService.GetBookByIdAsync(id);
     
        ReadBook readBook = ReadBook.FromBook(book);
        return Ok(readBook);
    }
    
}