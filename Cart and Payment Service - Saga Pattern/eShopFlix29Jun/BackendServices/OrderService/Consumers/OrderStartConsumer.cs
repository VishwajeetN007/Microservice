using CommonLibrary.Messages.Commands;
using CommonLibrary.Messages.Events;
using MassTransit;

namespace OrderService.Consumers
{
    public class OrderStartConsumer : IConsumer<IOrderStart>
    {
        public async Task Consume(ConsumeContext<IOrderStart> context)
        {
            await context.Publish<IOrderStarted>(new
            {
                context.Message.OrderId,
                context.Message.PaymentId,
                context.Message.CartId,
                context.Message.Products,
            });
        }
    }
}
