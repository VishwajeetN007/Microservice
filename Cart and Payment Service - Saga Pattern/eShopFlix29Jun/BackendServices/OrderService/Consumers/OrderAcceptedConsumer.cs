using CommonLibrary.Messages.Events;
using MassTransit;
using OrderService.Services.Interfaces;

namespace OrderService.Consumers
{
    public class OrderAcceptedConsumer : IConsumer<IOrderAccepted>
    {
        IOrderDataService _orderDataAccess;
        public OrderAcceptedConsumer(IOrderDataService orderDataAccess)
        {
            _orderDataAccess = orderDataAccess;
        }

        public async Task Consume(ConsumeContext<IOrderAccepted> context)
        {
            var order = context.Message;
            await _orderDataAccess.AcceptedOrder(order.OrderId, order.AcceptedDateTime);
        }
    }
}
