using CommonLibrary.Database;
using CommonLibrary.Messages.Commands;

namespace CommonLibrary.StateMachine
{
    public class StockValidate : IStockValidate
    {
        private readonly OrderState orderSaga;
        public StockValidate(OrderState orderState)
        {
            orderSaga = orderState;
        }
        public Guid OrderId => orderSaga.OrderId;
        public string PaymentId => orderSaga.PaymentId;
        public string Products => orderSaga.Products;
        public long CartId => orderSaga.CartId;
    }
}
