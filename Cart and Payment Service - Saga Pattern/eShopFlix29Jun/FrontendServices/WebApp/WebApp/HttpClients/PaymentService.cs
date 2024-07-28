using System.Text;
using System.Text.Json;
using WebApp.Models;

namespace WebApp.HttpClients
{
    public sealed class PaymentService
    {
        HttpClient _client;
        public PaymentService(HttpClient httpClient)
        {
            _client = httpClient;
        }

        public async Task<string> CreateOrderAsync(RazorPayOrder model)
        {
            StringContent content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("payment/CreateOrder", content);
            if (response.IsSuccessStatusCode)
            {
                string OrderId = response.Content.ReadAsStringAsync().Result;
                return OrderId;
            }
            return null;
        }

        public async Task<bool> SavePaymentDetailsAsync(TransactionModel model)
        {
            StringContent content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("payment/SavePaymentDetails", content);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<string> VerifyPaymentAsync(PaymentConfirmModel model)
        {
            StringContent content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("payment/VerifyPayment", content);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            return "";
        }
    }
}
