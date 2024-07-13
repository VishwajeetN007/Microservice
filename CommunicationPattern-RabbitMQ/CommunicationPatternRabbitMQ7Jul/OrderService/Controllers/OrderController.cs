using MassTransit;
using MassTransit.Transports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IPublishEndpoint _publishEndPoint;
        //IConfiguration _configuration;
        //ISendEndpointProvider _sendEndpointProvider;

        //public OrderController(ISendEndpointProvider sendEndpointProvider, 
        //                       IConfiguration configuration)
        //{
        //    _sendEndpointProvider = sendEndpointProvider;
        //    _configuration = configuration;
        //}

        public OrderController(IPublishEndpoint publishEndPoint)
        {
            _publishEndPoint = publishEndPoint;
        }

        //[HttpPost]
        //public async Task<IActionResult> Create(Order order)
        //{
        //    string exchange = _configuration["ServiceBus:Exchange"];
        //    string routingKey = _configuration["ServiceBus:RoutingKey"];
        //    var sendEndPoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"exchange:{exchange}"));

        //    await sendEndPoint.Send(order, context =>
        //    {
        //        context.SetRoutingKey(routingKey);
        //    });

        //    return Ok();
        //}

        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            order.OrderId = Guid.NewGuid();
            await _publishEndPoint.Publish(order);
            return Ok();
        }
    }
}
