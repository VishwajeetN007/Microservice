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

        /// <summary>
        /// Syntax with property arrow (line 35) is equivalent to below syntax.
        /// </summary>
        //public Guid OrderId
        //{
        //    get
        //    {
        //        return orderSaga.OrderId;
        //    }
        //}


        /// <summary>
        /// Syntax with get and set property arrow.
        /// </summary>
        //public Guid OrderId
        //{
        //    get => orderSaga.OrderId;
        //    set => orderSaga.OrderId = value;
        //}

        public Guid OrderId => orderSaga.OrderId;
        public string PaymentId => orderSaga.PaymentId;
        public string Products => orderSaga.Products;
        public long CartId => orderSaga.CartId;
    }
}
