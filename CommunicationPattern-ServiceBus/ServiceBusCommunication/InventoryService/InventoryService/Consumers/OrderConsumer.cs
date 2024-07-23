
using Azure.Messaging.ServiceBus;
using InventoryService.Data;
using InventoryService.Models;
using System.Text.Json;

namespace InventoryService.Consumers
{
    public class OrderConsumer : IOrderConsumer
    {
        ServiceBusClient _serviceBusClient;
        IConfiguration _configuration;
        ServiceBusProcessor _processor;
        public OrderConsumer(IConfiguration configuration)
        {
            _configuration = configuration;
            _serviceBusClient = new ServiceBusClient(_configuration["ServiceBus:ConnectionString"]);
            _processor = _serviceBusClient.CreateProcessor(_configuration["ServiceBus:QueueName"]);
        }

        /// <summary>
        /// Handler to handle the messages.
        /// </summary>
        /// <returns></returns>
        public async Task RegisterReceiveMessageHandler()
        {
            _processor.ProcessMessageAsync += MessageHandler;
            _processor.ProcessErrorAsync += ErrorHandler;

            await _processor.StartProcessingAsync();
        }

        /// <summary>
        /// Handle errors when receiving messages.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private async Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            await Task.CompletedTask;
        }

        /// <summary>
        /// Handle successfully recieve messages.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            Order message = JsonSerializer.Deserialize<Order>(body);
            // Process the message
            MyData.Orders.Add(message);

            // Complete the message. Message is deleted from the queue.
            await args.CompleteMessageAsync(args.Message);
            await Task.CompletedTask;
        }
    }
}
