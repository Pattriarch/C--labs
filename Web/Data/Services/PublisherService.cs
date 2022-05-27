using Microsoft.EntityFrameworkCore;
using Web.Data.Context;
using Web.Data.DataSource;
using Web.Data.Models;

namespace Web.Data.Services;

public class PublisherService
{
    private EducationContext _context;
    public PublisherService(EducationContext context)
    {
        _context = context;
    }

    public async Task<Publisher?> AddPublisher(PublisherDTO publisher)
    {
        Publisher npublisher = new Publisher
        {
            Name = publisher.Name,
        };

        var result = _context.Publishers.Add(npublisher);
        await _context.SaveChangesAsync();
        return await Task.FromResult(result.Entity);
    }

    public async Task<Publisher?> GetPublisher(int id)
    {
        var result = _context.Publishers.Include(a=>a.Books).FirstOrDefault(publisher => publisher.Id==id);
        return await Task.FromResult(result);
    }



    public async Task<List<Publisher>> GetPublishers()
    {
        var result = await _context.Publishers.Include(b=>b.Books).ToListAsync();
        return await Task.FromResult(result);
    }

    public async Task<Publisher?> UpdatePublisher(int id, Publisher updatedPublisher)
    {
        var publisher = await _context.Publishers.Include(b=>b.Books).FirstOrDefaultAsync(au => au.Id == id);
        if (publisher != null)
        {
            publisher.Name = updatedPublisher.Name;
            if (updatedPublisher.Books.Any())
            {
                publisher.Books  = _context.Books.ToList().IntersectBy(updatedPublisher.Books, book => book).ToList();
            }
            _context.Publishers.Update(publisher);
            _context.Entry(publisher).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return publisher;
        }

        return null;
    }

    public async Task<bool> DeletePublisher(int id)
    {
        var publisher = await _context.Publishers.FirstOrDefaultAsync(au => au.Id == id);
        if (publisher != null)
        {
            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }
}