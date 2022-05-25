using Web.Data.Models;

public interface IPublishersProvider
{
    Task<List<Publisher>?> GetAll();
    Task<Publisher?> GetOne(int id);
    Task<bool> Add(Publisher item);
    Task<Publisher?> Edit(Publisher item);
    Task<bool> Remove(int id);
}
