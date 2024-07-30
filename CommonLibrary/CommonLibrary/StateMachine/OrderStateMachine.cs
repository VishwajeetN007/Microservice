using CommonLibrary.Database;
using CommonLibrary.Messages.Commands;
using CommonLibrary.Messages.Events;
using MassTransit;

namespace CommonLibrary.StateMachine
{
    public class OrderStateMachine : MassTransitStateMachine<OrderState>
    {
        public OrderStateMachine()
        {
            Event(() => OrderStarted, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => StockValidated, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => OrderAccepted, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => OrderCancelled, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => PaymentCancelled, x => x.CorrelateById(m => m.Message.OrderId));

            InstanceState(x => x.CurrentState);

            Initially(
               When(OrderStarted)
                .Then(ctx =>
                {
                    ctx.Instance.OrderId = ctx.Data.OrderId;
                    ctx.Instance.PaymentId = ctx.Data.PaymentId;
                    ctx.Instance.CartId = ctx.Data.CartId;
                    ctx.Instance.Products = ctx.Data.Products;
                    ctx.Instance.OrderCreationDateTime = DateTime.Now;
                })
                .TransitionTo(Started) //save state to db
                .Publish(ctx => new StockValidate(ctx.Instance)));

            During(Started,
              When(OrderCancelled)
              .Then(ctx => ctx.Instance.OrderCancelDateTime = DateTime.Now)
              .TransitionTo(Canceled)); //save state to db

            During(Started,
             When(OrderAccepted)
             .Then(ctx => ctx.Instance.OrderAcceptDateTime = DateTime.Now)
             .TransitionTo(Accepted)); //save state to db
        }

        public State Started { get; set; }
        public State Validated { get; set; }
        public State Accepted { get; set; }
        public State Canceled { get; set; }

        public Event<IOrderStarted> OrderStarted { get; set; }
        public Event<IStockValidated> StockValidated { get; set; }
        public Event<IOrderAccepted> OrderAccepted { get; set; }
        public Event<IOrderCancelled> OrderCancelled { get; set; }
        public Event<IPaymentCancelled> PaymentCancelled { get; set; }
    }
}
