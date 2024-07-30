
using Azure.Messaging.ServiceBus;
using System.Text.Json;
using System.Text;
using OrderService.Models;

namespace OrderService.ServiceBus
{
    public class OrderConsumer : IOrderConsumer
    {
        ServiceBusClient _serviceBusClient;
        IConfiguration _configuration;
        ServiceBusProcessor _processor;
        HttpClient _client;
        public OrderConsumer(IConfiguration configuration)
        {
            _configuration = configuration;
            _serviceBusClient = new ServiceBusClient(_configuration["ConnectionStrings:ServiceBusConnection"]);
            var options = new ServiceBusProcessorOptions
            {
                // By default AutoCompleteMessages is set to true
                AutoCompleteMessages = false
            };
            _processor = _serviceBusClient.CreateProcessor(_configuration["ServiceBus:Queue"], options);
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_configuration["ApiAddress:Order"]);
        }

        public async Task RegisterReceiveMessageHandler()
        {
            _processor.ProcessMessageAsync += MessageHandler;
            _processor.ProcessErrorAsync += ErrorHandler;

            await _processor.StartProcessingAsync();
        }

        async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();

            OrderMessage model = JsonSerializer.Deserialize<OrderMessage>(body);
            _ = _client.PostAsync(_client.BaseAddress + "Order/CreateOrder", new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json"));

            //// complete the message. message is deleted from the queue. 
             await args.CompleteMessageAsync(args.Message);
        }

        // handle any errors when receiving messages
        Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
    }
}
