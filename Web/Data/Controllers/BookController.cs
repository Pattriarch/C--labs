using Microsoft.AspNetCore.Mvc;
using Web.Data.DataSource;
using Web.Data.Dtos;
using Web.Data.Models;
using Web.Data.Services;

namespace Web.Data.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly BookService _context;

    public BookController(BookService context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBook()
    {
        return await _context.GetBooks();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBook(int id)
    {
        var book = await _context.GetBook(id);

        if (book == null)
        {
            return NotFound();
        }

        return book;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Book>> PutBook(int id, [FromBody] Book book)
    {
        var result = await _context.UpdateBook(id, book);
        if (result == null)
        {
            return BadRequest();
        }
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Book>> PostBook([FromBody]BookDTO book)
    {
        var result = await _context.AddBook(book);
        if (result == null)
        {
            BadRequest();
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _context.DeleteBook(id);
        if (book)
        {
            return Ok();
        }
        return BadRequest();
    }
}