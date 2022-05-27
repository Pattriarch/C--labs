using Web.Data.Models;

namespace Blazor.Services;

public interface ICategoriesProvider
{
    Task<List<Category>> GetAll();
    Task<Category?> GetOne(int id);
    Task<bool> Add(Category item);
    Task<Category?> Edit(Category item);
    Task<bool> Remove(int id);
}