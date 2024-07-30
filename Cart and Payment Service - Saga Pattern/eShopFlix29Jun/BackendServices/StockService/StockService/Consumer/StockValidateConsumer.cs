using CommonLibrary.Messages.Commands;
using CommonLibrary.Messages.Events;
using MassTransit;
using StockService.Services.Interfaces;

namespace StockService.Consumer
{
    public class StockValidateConsumer : IConsumer<IStockValidate>
    {
        IStockDataService _stockService;
        public StockValidateConsumer(IStockDataService stockDataService)
        {
            _stockService = stockDataService;
        }

        public async Task Consume(ConsumeContext<IStockValidate> context)
        {
            try
            {
                var data = context.Message;
                if (data != null)
                {
                    //check stock
                    var products = data.Products.Split(",").ToArray();
                    bool isStockAvailable = true;
                    foreach (var product in products)
                    {
                        var productDetails = product.Split(":");
                        int productId = Convert.ToInt32(productDetails[0]);
                        int quantity = Convert.ToInt32(productDetails[1]);
                        isStockAvailable = _stockService.CheckStockAvailibility(productId, quantity);
                        if (!isStockAvailable)
                        {
                            break;
                        }
                    }
                    if (!isStockAvailable)
                    {
                        //cancel order
                        await context.Publish<IOrderCancelled>(new
                        {
                            OrderId = data.OrderId,
                            PaymentId = data.PaymentId,
                            CartId = data.CartId
                        });
                    }
                    else
                    {
                        //reserve stock
                        foreach (var product in products)
                        {
                            var productDetails = product.Split(":");
                            int productId = Convert.ToInt32(productDetails[0]);
                            int quantity = Convert.ToInt32(productDetails[1]);
                            _stockService.ReserveStock(productId, quantity);
                        }

                        //go to the next step               
                        await context.Publish<IOrderAccepted>(new
                        {
                            OrderId = data.OrderId,
                            PaymentId = data.PaymentId,
                            CartId = data.CartId
                        });
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}
