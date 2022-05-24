using Microsoft.EntityFrameworkCore;
using Web.Data.Context;
using Web.Data.Dtos;
using Web.Data.Models;

namespace Web.Data.Services;

public class AuthorService
{
    private EducationContext _context;
    public AuthorService(EducationContext context)
    {
        _context = context;
    }

    public async Task<Author?> AddAuthor(AuthorDTO author)
    {
        Author nauthor = new Author
        {
            Name = author.Name,
            Description = author.Description,
        };
        if (author.Books.Any())
        {
            nauthor.Books  = _context.Books.ToList().IntersectBy(author.Books, book => book.Id).ToList();
        }
        var result = _context.Authors.Add(nauthor);
        await _context.SaveChangesAsync();
        return await Task.FromResult(result.Entity);
    }

    public async Task<Author?> GetAuthor(int id)
    {
        var result = _context.Authors.Include(a=>a.Books).FirstOrDefault(author => author.Id==id);
        return await Task.FromResult(result);
    }

    public async Task<List<Author>> GetAuthors()
    {
        var result = await _context.Authors.Include(b=>b.Books).ToListAsync();
        return await Task.FromResult(result);
    }

    public async Task<Author?> UpdateAuthor(int id, Author updatedAuthor)
    {
        var author = await _context.Authors.Include(b=>b.Books).FirstOrDefaultAsync(au => au.Id == id);
        if (author != null)
        {
            author.Name = updatedAuthor.Name;
            author.Description = updatedAuthor.Description;
            if (updatedAuthor.Books.Any())
            {
                author.Books  = _context.Books.ToList().IntersectBy(updatedAuthor.Books, affiliation => affiliation).ToList();
            }
            _context.Authors.Update(author);
            _context.Entry(author).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return author;
        }

        return null;
    }

    public async Task<bool> DeleteAuthor(int id)
    {
        var author = await _context.Authors.FirstOrDefaultAsync(au => au.Id == id);
        if (author != null)
        {
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }

}