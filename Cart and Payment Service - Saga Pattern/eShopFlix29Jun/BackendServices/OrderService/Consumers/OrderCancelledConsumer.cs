using CommonLibrary.Messages.Events;
using MassTransit;
using OrderService.Services.Interfaces;

namespace OrderService.Consumers
{
    public class OrderCancelledConsumer : IConsumer<IOrderCancelled>
    {
        IOrderDataService _orderDataAccess;
        public OrderCancelledConsumer(IOrderDataService orderDataAccess)
        {
            _orderDataAccess = orderDataAccess;
        }
        public async Task Consume(ConsumeContext<IOrderCancelled> context)
        {
            var data = context.Message;
            await _orderDataAccess.DeleteOrder(data.OrderId);
        }
    }
}
