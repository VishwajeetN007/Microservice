using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using System.Text.Json;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        ServiceBusClient _serviceBusClient;
        IConfiguration _configuration;
        ServiceBusSender _sender;
        public OrderController(IConfiguration configuration)
        {
            _configuration = configuration;
            _serviceBusClient = new ServiceBusClient(_configuration["ServiceBus:ConnectionString"]);
            _sender = _serviceBusClient.CreateSender(_configuration["ServiceBus:QueueName"]);
        }


        /// <summary>
        /// Create Order. Its acts as sender to send a messages.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            string strMessage = JsonSerializer.Serialize(order);
            var message = new ServiceBusMessage(strMessage);
            await _sender.SendMessageAsync(message);
            return Ok();
        }
    }
}
