using Microsoft.EntityFrameworkCore;
using Web.Data.Context;
using Web.Data.DataSource;
using Web.Data.Dtos;
using Web.Data.Models;

namespace Web.Data.Services;

public class BookService
{
    private EducationContext _context;
    public BookService(EducationContext context)
    {
        _context = context;
    }

    public async Task<Book?> AddBook(BookDTO book)
    {
        Book nbook = new Book
        {
            Name = book.Name,
            Description = book.Description,
            Article = book.Article
        };
        if (book.Category != null)
        {
            nbook.Category = _context.Categories.Find(book.Category);
        }
        if (book.Publisher != null)
        {
            nbook.Publisher = _context.Publishers.Find(book.Publisher);
        }
        if (book.Authors.Any())
        {
            nbook.Authors  = _context.Authors.ToList().IntersectBy(book.Authors, author => author.Id).ToList();
        }
        var result = _context.Books.Add(nbook);
        await _context.SaveChangesAsync();
        return await Task.FromResult(result.Entity);
    }

    public async Task<Book?> GetBook(int id)
    {
        var result = _context.Books
            .Include(a=>a.Category)
            .Include(b=>b.Publisher)
            .Include(c=>c.Authors)
            .FirstOrDefault(book => book.Id==id);
        return await Task.FromResult(result);
    }



    public async Task<List<Book>> GetBooks()
    {
        var result = await _context.Books
            .Include(a=>a.Category)
            .Include(b=>b.Publisher)
            .Include(c=>c.Authors)
            .ToListAsync();
        return await Task.FromResult(result);
    }

    public async Task<Book?> UpdateBook(int id, Book updatedBook)
    {
        var book = await _context.Books
            .Include(a=>a.Category)
            .Include(b=>b.Publisher)
            .Include(c=>c.Authors)
            .FirstOrDefaultAsync(au => au.Id == id);
        if (book != null)
        {
            book.Name = updatedBook.Name;
            book.Description = updatedBook.Description;
            book.Article = updatedBook.Article;
            if (updatedBook.Category != null)
            {
                book.Category = _context.Categories.Find(updatedBook.Category);
            }
            if (updatedBook.Publisher != null)
            {
                book.Publisher = _context.Publishers.Find(updatedBook.Publisher);
            }
            if (updatedBook.Authors.Any())
            {
                book.Authors  = _context.Authors.ToList().IntersectBy(updatedBook.Authors, author => author).ToList();
            }
            _context.Books.Update(book);
            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return book;
        }

        return null;
    }

    public async Task<bool> DeleteBook(int id)
    {
        var book = await _context.Books.FirstOrDefaultAsync(au => au.Id == id);
        if (book != null)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }
}