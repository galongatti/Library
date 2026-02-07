using Library.Model.DTO;
using Library.Model.Entities;
using Library.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;


[ApiController]
[Route("api/books")]
public class BookController(IBookService bookService) : ControllerBase
{
    
    [HttpPost]
    [Authorize(Roles = "InternalUser")]
    public async Task<IActionResult> Create(CreateBook model)
    {
        Book book = await bookService.CreateBookAsync(model);
        return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
    }


    [HttpGet]
    [Authorize(Roles = "InternalUser,customer")]
    public async Task<IActionResult> Get()
    {
        List<Book> books = await bookService.GetBooksAsync();
        List<ReadBook> readBooks = ReadBook.FromBooks(books);
        return Ok(readBooks);
    }
    
    
    [HttpPut("{id:int}")]
    [Authorize(Roles = "InternalUser")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBook model)
    {
        _ = await bookService.UpdateBookAsync(id, model);
        return Ok();
        
    }
    
    [HttpPut("{id:int}/authors")]
    [Authorize(Roles = "InternalUser")]
    public async Task<IActionResult> UpdateBookAuthors([FromRoute] int id, [FromBody] UpdateBookAuthors model)
    {
        _ = await bookService.UpdateBookAuthorsAsync(id, model);
        return Ok();
    }



    [HttpPost("{id:int}/copies")]
    [Authorize(Roles = "InternalUser")]
    public async Task<IActionResult> AddCopy([FromRoute] int id, [FromBody] AddCopyModel model)
    {
        BookCopy copy = await bookService.AddCopyAsync(id, model.Barcode);
        return CreatedAtAction(nameof(Get), new { id }, copy);
    }

    [HttpDelete("{bookId:int}/copies/{copyId:int}")]
    [Authorize(Roles = "InternalUser")]
    public async Task<IActionResult> RemoveCopy([FromRoute] int bookId, [FromRoute] int copyId)
    {
        _ = await bookService.RemoveCopyAsync(copyId);
        return Ok();
    }

    [HttpGet("{id:int}/copies/available-count")]
    [Authorize(Roles = "InternalUser,customer")]
    public async Task<IActionResult> AvailableCount([FromRoute] int id)
    {
        int count = await bookService.CountAvailableCopiesAsync(id);
        return Ok(new { AvailableCopies = count });
    }
    
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "InternalUser")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await bookService.DeleteBookAsync(id);
        return Ok();
    }
    
    [HttpGet("{id:int}")]
    [Authorize(Roles = "InternalUser,customer")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        Book? book = await bookService.GetBookByIdAsync(id);
        
        ReadBook readBook = ReadBook.FromBook(book);
        return Ok(readBook);
    }
    
}