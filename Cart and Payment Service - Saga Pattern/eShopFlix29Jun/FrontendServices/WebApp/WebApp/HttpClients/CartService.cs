using Newtonsoft.Json;
using System.Text;
using WebApp.Models;

namespace WebApp.HttpClients
{
    public sealed class CartService
    {
        HttpClient _client;
        public CartService(HttpClient httpClient)
        {
            _client = httpClient;
        }

        public async Task<CartModel> GetUserCartAsync(long UserId)
        {
            var response = await _client.GetAsync("cart/GetUserCart/" + UserId);
            if (response.IsSuccessStatusCode && response.Content!=null)
            {
                var cartData = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(cartData))
                {
                    var cart = JsonConvert.DeserializeObject<CartModel>(cartData);
                    return cart;
                }
            }
            return null;
        }

        public async Task<CartModel> GetCartAsync(long CartId)
        {
            var cart = await _client.GetFromJsonAsync<CartModel>("cart/getcart/" + CartId);
            return cart ?? null;
        }

        public async Task<bool> MakeCartInActiveAsync(long CartId)
        {
            return await _client.GetFromJsonAsync<bool>("cart/makeinactive/" + CartId);
        }

        public async Task<CartModel> AddToCartAsync(CartItemModel item, long UserId)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("cart/additem/" + UserId, content);
            if (response.IsSuccessStatusCode)
            {
                var cart = await response.Content.ReadFromJsonAsync<CartModel>();
                return cart;
            }
            return null;
        }

        public async Task<int> DeleteCartItemAsync(long CartId, int ItemId)
        {
            var status = await _client.DeleteFromJsonAsync<int>("cart/deleteItem/" + CartId + "/" + ItemId);
            return status;
        }

        public async Task<int> UpdateQuantity(long CartId, int ItemId, int Quantity)
        {
            var status = await _client.GetFromJsonAsync<int>("cart/UpdateQuantity/" + CartId + "/" + ItemId + "/" + Quantity);
            return status;
        }
    }
}
