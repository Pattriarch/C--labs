using System.Net.Http.Json;
using Newtonsoft.Json;
using Web.Data.Models;


public class PublishersProvider : IPublishersProvider
{
    private HttpClient _client;
    public PublishersProvider(HttpClient client)
    {
        _client = client;
    }
    
    public async Task<List<Publisher>?> GetAll()
    {
        return await _client.GetFromJsonAsync<List<Publisher>>("/api/Publisher");
    }

    public async Task<Publisher?> GetOne(int id)
    {
        return await _client.GetFromJsonAsync<Publisher>($"/api/Publisher/{id}");
    }

    public async Task<bool> Add(Publisher item)
    {
        string data = JsonConvert.SerializeObject(item);
        StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
        var response = await _client.PostAsync($"/api/Publisher", httpContent);
        return await Task.FromResult(response.IsSuccessStatusCode);
    }

    public async Task<Publisher?> Edit(Publisher item)
    {
        string data = JsonConvert.SerializeObject(item);
        StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
        var response = await _client.PutAsync($"/api/Publisher", httpContent);
        Publisher? publisher = JsonConvert.DeserializeObject<Publisher>(response.Content.ReadAsStringAsync().Result);
        return await Task.FromResult(publisher);
    }

    public async Task<bool> Remove(int id)
    {
        var delete = await _client.DeleteAsync($"/api/Publisher/${id}");

        return await Task.FromResult(delete.IsSuccessStatusCode);
    }
}
