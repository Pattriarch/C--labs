using Microsoft.EntityFrameworkCore;
using Web.Data.Context;
using Web.Data.DataSource;
using Web.Data.Models;

namespace Web.Data.Services;

public class CategoryService
{
    private EducationContext _context;
    public CategoryService(EducationContext context)
    {
        _context = context;
    }

    public async Task<Category?> AddCategory(CategoryDTO category)
    {
        Category ncategory = new Category
        {
            Name = category.Name,
        };

        var result = _context.Categories.Add(ncategory);
        await _context.SaveChangesAsync();
        return await Task.FromResult(result.Entity);
    }

    public async Task<Category?> GetCategory(int id)
    {
        var result = _context.Categories.Include(b=>b.Books).FirstOrDefault(category => category.Id==id);
        return await Task.FromResult(result);
    }



    public async Task<List<Category>> GetCategories()
    {
        var result = await _context.Categories.Include(a=>a.Books).ToListAsync();
        return await Task.FromResult(result);
    }

    public async Task<Category?> UpdateCategory(int id, Category updatedCategory)
    {
        var category = await _context.Categories.Include(b=>b.Books).FirstOrDefaultAsync(au => au.Id == id);
        if (category != null)
        {
            category.Name = updatedCategory.Name;
            if (updatedCategory.Books.Any())
            {
                category.Books  = _context.Books.ToList().IntersectBy(updatedCategory.Books, book => book).ToList();
            }
            _context.Categories.Update(category);
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return category;
        }

        return null;
    }

    public async Task<bool> DeleteCategory(int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(au => au.Id == id);
        if (category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }
}