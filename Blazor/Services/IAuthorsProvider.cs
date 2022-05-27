using Web.Data.Models;

namespace Blazor.Services;

public interface IAuthorsProvider
{
    Task<List<Author>> GetAll();
    Task<Author?> GetOne(int id);
    Task<bool> Add(Author item);
    Task<Author?> Edit(Author item);
    Task<bool> Remove(int id);
}