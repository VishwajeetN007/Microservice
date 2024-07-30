using SupportApp.Models;
using System.Text;
using System.Text.Json;

namespace SupportApp.HttpClients
{
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
