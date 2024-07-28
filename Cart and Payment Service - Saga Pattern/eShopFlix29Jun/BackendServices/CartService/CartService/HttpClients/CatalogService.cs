using CartService.Models;

namespace CartService.HttpClients
{
    public sealed class CatalogService
    {
        HttpClient _client;
        public CatalogService(HttpClient httpClient)
        {
            _client = httpClient;
        }

        public async Task<ProductModel> GetProductAsync(int id)
        {
            var products = await _client.GetFromJsonAsync<ProductModel>("product/get/"+id);
            return products ?? null;
        }
    }
}
