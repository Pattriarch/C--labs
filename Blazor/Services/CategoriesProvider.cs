using System.Net.Http.Json;
using Newtonsoft.Json;
using Web.Data.Models;

namespace Blazor.Services;

public class CategoriesProvider : ICategoriesProvider
{
    private HttpClient _client;
    public CategoriesProvider(HttpClient client)
    {
        _client = client;
    }
    
    public async Task<List<Category>> GetAll()
    {
        return await _client.GetFromJsonAsync<List<Category>>("/api/Category");
    }

    public async Task<Category?> GetOne(int id)
    {
        return await _client.GetFromJsonAsync<Category>($"/api/Category/{id}");
    }

    public async Task<bool> Add(Category item)
    {
        string data = JsonConvert.SerializeObject(item);
        StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
        var response = await _client.PostAsync($"/api/Category", httpContent);
        return await Task.FromResult(response.IsSuccessStatusCode);
    }

    public async Task<Category?> Edit(Category item)
    {
        string data = JsonConvert.SerializeObject(item);
        StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
        var response = await _client.PutAsync($"/api/Category", httpContent);
        Category? excursion = JsonConvert.DeserializeObject<Category>(response.Content.ReadAsStringAsync().Result);
        return await Task.FromResult(excursion);
    }

    public async Task<bool> Remove(int id)
    {
        var delete = await _client.DeleteAsync($"/api/Category/${id}");

        return await Task.FromResult(delete.IsSuccessStatusCode);
    }
}