using OrderService.Models;

namespace OrderService.HttpClients
{
    public class CartService
    {
        HttpClient _client;
        public CartService(HttpClient httpClient)
        {
            _client = httpClient;
        }

        public async Task<IEnumerable<CartItemModel>> GetCartItemsAsync(long id)
        {
            var items = await _client.GetFromJsonAsync<IEnumerable<CartItemModel>>("cart/getitems/" + id);
            return items ?? null;
        }
    }
}
