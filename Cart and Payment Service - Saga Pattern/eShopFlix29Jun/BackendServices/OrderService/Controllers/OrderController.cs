using CommonLibrary.Messages.Commands;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Database.Entities;
using OrderService.Models;
using OrderService.Services.Interfaces;

namespace OrderService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderDataService _orderDataService;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        public OrderController(IOrderDataService orderDataService, ISendEndpointProvider sendEndpointProvider)
        {
            _orderDataService = orderDataService;
            _sendEndpointProvider = sendEndpointProvider;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderMessage model)
        {
            try
            {
                Order order = new Order();
                order.OrderId = Guid.NewGuid();
                order.PaymentId = model.PaymentId;
                order.UserId = model.UserId;
                order.CreatedDate = DateTime.Now;

                _orderDataService.SaveOrder(order, model.CartId);

                //SAGA Pattern

                //var uri = new Uri("queue:orderstart");
                var uri = new Uri("StartQueue:orderstart"); //// Refer appsettings.json section.
                var endpoint = await _sendEndpointProvider.GetSendEndpoint(uri);
                await endpoint.Send<IOrderStart>(new
                {
                    OrderId = order.OrderId,
                    PaymentId = model.PaymentId,
                    Products = model.Products,
                    CartId = model.CartId,
                    UserId = model.UserId
                });

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
