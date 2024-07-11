using SupportApp.Models;
using System.Text;
using System.Text.Json;

namespace SupportApp.HttpClients
{
    public class ProductService
    {
        HttpClient _client;
        public ProductService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<ProductModel>> GetAll(LoginModel loginModel)
        {
            var response = await _client.GetAsync("product/getall");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var products = JsonSerializer.Deserialize<IEnumerable<ProductModel>>(data);
                return products;
            }
            return null;
        }
    }
}
