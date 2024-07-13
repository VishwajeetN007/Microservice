using MassTransit;
using Models;
using InventoryService.Data;

namespace InventoryService.Consumers
{
    public class OrderConsumer : IConsumer<Order>
    {

        public async Task Consume(ConsumeContext<Order> context)
        {
            var order = context.Message;

            // Check Inventory Availability

            MyData.Data.Add(order);
            await Task.CompletedTask;
        }
    }
}
