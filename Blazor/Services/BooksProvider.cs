using System.Net.Http.Json;
using Newtonsoft.Json;
using Web.Data.Models;

namespace Blazor.Services;

public class BooksProvider : IBooksProvider
{
    private HttpClient _client;
    public BooksProvider(HttpClient client)
    {
        _client = client;
    }
    
    public async Task<List<Book>?> GetAll()
    {
        return await _client.GetFromJsonAsync<List<Book>>("/api/Book");
    }

    public async Task<Book?> GetOne(int id)
    {
        return await _client.GetFromJsonAsync<Book>($"/api/Book/{id}");
    }

    public async Task<bool> Add(Book item)
    {
        string data = JsonConvert.SerializeObject(item);
        StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
        var response = await _client.PostAsync($"/api/Book", httpContent);
        return await Task.FromResult(response.IsSuccessStatusCode);
    }

    public async Task<Book?> Edit(Book item)
    {
        string data = JsonConvert.SerializeObject(item);
        StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
        var response = await _client.PutAsync($"/api/Book", httpContent);
        Book? excursion = JsonConvert.DeserializeObject<Book>(response.Content.ReadAsStringAsync().Result);
        return await Task.FromResult(excursion);
    }

    public async Task<bool> Remove(int id)
    {
        var delete = await _client.DeleteAsync($"/api/Book/${id}");

        return await Task.FromResult(delete.IsSuccessStatusCode);
    }
}