using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace HackatonAdminPanel.Services;
public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://yourapiurl.com/api/") // Replace with your API base URL
        };
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<List<UserDto>> GetAllDistributorsAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("UserData/GetAllDistributors");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<UserDto>>(content);
    }

    public async Task<bool> AddUserAsync(UserRequest user)
    {
        string json = JsonSerializer.Serialize(user);
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _httpClient.PostAsync("UserData/AddUser", content);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SoftDeleteUserAsync(int id)
    {
        HttpResponseMessage response = await _httpClient.PutAsync($"UserData/SoftDeleteUser/{id}", null);
        return response.IsSuccessStatusCode;
    }
}

