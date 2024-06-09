using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace HackatonAdminPanel.Services;
public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7037/api/") // Replace with your API base URL
        };
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<List<UserDto>> GetAllDistributorsAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("UserData/GetAllDistributors");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        return System.Text.Json.JsonSerializer.Deserialize<List<UserDto>>(content);
    }

    public async Task<bool> AddUserAsync(UserRequest user)
    {
        string json = System.Text.Json.JsonSerializer.Serialize(user);
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _httpClient.PostAsync("UserData/AddUser", content);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SoftDeleteUserAsync(int id)
    {
        HttpResponseMessage response = await _httpClient.PutAsync($"UserData/SoftDeleteUser/{id}", null);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> MintTokensAsync(string walletId, string url)
    {
        var response = await _httpClient.GetAsync($"Nft/MintTokens?walletId={walletId}&url={url}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return response.IsSuccessStatusCode; //JsonSerializer.Deserialize<List<Root>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task<Root> GetTokensAsync(string accountId)
    {
        var response = await _httpClient.GetAsync($"Nft/GetTokens?accountId={accountId}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Root>(content);
    }
}

