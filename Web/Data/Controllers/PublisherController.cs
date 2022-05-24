using Microsoft.AspNetCore.Mvc;
using Web.Data.DataSource;
using Web.Data.Dtos;
using Web.Data.Models;
using Web.Data.Services;

namespace Web.Data.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PublisherController : ControllerBase
{
    private readonly PublisherService _context;

    public PublisherController(PublisherService context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Publisher>>> GetPublisher()
    {
        return await _context.GetPublishers();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Publisher>> GetPublisher(int id)
    {
        var publisher = await _context.GetPublisher(id);

        if (publisher == null)
        {
            return NotFound();
        }

        return publisher;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Publisher>> PutPublisher(int id, [FromBody] Publisher publisher)
    {
        var result = await _context.UpdatePublisher(id, publisher);
        if (result == null)
        {
            return BadRequest();
        }
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Publisher>> PostPublisher([FromBody]PublisherDTO publisher)
    {
        var result = await _context.AddPublisher(publisher);
        if (result == null)
        {
            BadRequest();
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePublisher(int id)
    {
        var publisher = await _context.DeletePublisher(id);
        if (publisher)
        {
            return Ok();
        }
        return BadRequest();
    }
}