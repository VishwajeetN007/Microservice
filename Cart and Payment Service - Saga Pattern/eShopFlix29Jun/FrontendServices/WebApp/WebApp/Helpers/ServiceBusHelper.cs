using Azure.Messaging.ServiceBus;
using System.Text.Json;
using WebApp.Messages;

namespace WebApp.Helpers
{
    public class ServiceBusHelper : IServiceBusHelper
    {
        ServiceBusClient _serviceBusClient;
        IConfiguration _configuration;
        ServiceBusSender _sender;
        public ServiceBusHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            _serviceBusClient = new ServiceBusClient(_configuration["ConnectionStrings:ServiceBusConnection"]);
            _sender = _serviceBusClient.CreateSender(_configuration["ServiceBus:OrderQueue"]);
        }
        public async Task SendPaymentMessage(OrderMessage payload)
        {
            string data = JsonSerializer.Serialize(payload);
            var message = new ServiceBusMessage(data);

            await _sender.SendMessageAsync(message);
        }
    }
}
