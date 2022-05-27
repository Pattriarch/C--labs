using Web.Data.Models;

namespace Blazor.Services;

public interface IBooksProvider
{
    Task<List<Book>?> GetAll();
    Task<Book?> GetOne(int id);
    Task<bool> Add(Book item);
    Task<Book?> Edit(Book item);
    Task<bool> Remove(int id);
}