using WebApp.Models;

namespace WebApp.HttpClients
{
    public class CatalogService
    {
        HttpClient _client;
        public CatalogService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<ProductModel>> GetProducts()
        {
            //// "catalog/getall" associate with the ocelot.json api gateway.
            var products = await _client.GetFromJsonAsync<IEnumerable<ProductModel>>("catalog/getall");
            return products ?? [];
        }
    }
}
