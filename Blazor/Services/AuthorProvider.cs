using System.Net.Http.Json;
using Newtonsoft.Json;
using Web.Data.Models;

namespace Blazor.Services;

public class AuthorProvider
{
    private HttpClient _client;
    public AuthorProvider(HttpClient client)
    {
        _client = client;
    }
    
    public async Task<List<Author>?> GetAll()
    {
        return await _client.GetFromJsonAsync<List<Author>>("/api/Author");
    }

    public async Task<Author?> GetOne(int id)
    {
        return await _client.GetFromJsonAsync<Author>($"/api/Author/{id}");
    }

    public async Task<bool> Add(Author item)
    {
        string data = JsonConvert.SerializeObject(item);
        StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
        var response = await _client.PostAsync($"/api/Author", httpContent);
        return await Task.FromResult(response.IsSuccessStatusCode);
    }

    public async Task<Author?> Edit(Author item)
    {
        string data = JsonConvert.SerializeObject(item);
        StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
        var response = await _client.PutAsync($"/api/Author", httpContent);
        Author? excursion = JsonConvert.DeserializeObject<Author>(response.Content.ReadAsStringAsync().Result);
        return await Task.FromResult(excursion);
    }

    public async Task<bool> Remove(int id)
    {
        var delete = await _client.DeleteAsync($"/api/Author/${id}");

        return await Task.FromResult(delete.IsSuccessStatusCode);
    }
}