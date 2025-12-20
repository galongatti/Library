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
        bool ok = await bookService.UpdateBookAsync(id, model);
        
        if(ok) return Ok();
        
        return BadRequest();
    }
    
    [HttpPut("{id:int}/authors")]
    [Authorize(Roles = "InternalUser")]
    public async Task<IActionResult> UpdateBookAuthors([FromRoute] int id, [FromBody] UpdateBookAuthors model)
    {
        bool ok = await bookService.UpdateBookAuthorsAsync(id, model);
        
        if(ok) return Ok();
        
        return BadRequest();
    }

    [HttpGet("{id:int}/copies")]
    [Authorize(Roles = "InternalUser,customer")]
    public async Task<IActionResult> GetCopies([FromRoute] int id)
    {
        List<BookCopy> copies = await bookService.GetCopiesByBookIdAsync(id);
        List<ReadBookCopy> bookCopies = ReadBookCopy.FromBooksCopies(copies);
        return Ok(bookCopies);
    }

    [HttpPost("{id:int}/copies")]
    [Authorize(Roles = "InternalUser")]
    public async Task<IActionResult> AddCopy([FromRoute] int id, [FromBody] AddCopyModel model)
    {
        BookCopy copy = await bookService.AddCopyAsync(id, model.Barcode);
        return CreatedAtAction(nameof(GetCopies), new { id }, copy);
    }

    [HttpDelete("{bookId:int}/copies/{copyId:int}")]
    [Authorize(Roles = "InternalUser")]
    public async Task<IActionResult> RemoveCopy([FromRoute] int bookId, [FromRoute] int copyId)
    {
        bool ok = await bookService.RemoveCopyAsync(copyId);
        if (ok) return Ok();
        return BadRequest();
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
        bool ok = await bookService.DeleteBookAsync(id);
        
        if(ok) return Ok();
        
        return BadRequest();
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