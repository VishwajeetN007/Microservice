using System.Text;
using System.Text.Json;
using WebApp.Models;

namespace WebApp.HttpClients
{

    /// <summary>
    /// Http Client Factory (We can use single HttpClient for many Microservice Backend Services)
    /// Its called as Service Pattern which provides reusibility like utility function so we can use again and again.
    /// 
    /// Service Pattern use for Database layer and Repository Pattern use for Busniess Layer.
    /// </summary>
    public class AuthService
    {
        HttpClient _client;
        public AuthService(HttpClient client)
        {
            _client = client;
        }

        public async Task<UserModel> Login(LoginModel loginModel)
        {
            StringContent content = new StringContent(JsonSerializer.Serialize(loginModel), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("auth/login", content);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var user = JsonSerializer.Deserialize<UserModel>(data);
                return user;
            }
            return null;
        }
    }
}
