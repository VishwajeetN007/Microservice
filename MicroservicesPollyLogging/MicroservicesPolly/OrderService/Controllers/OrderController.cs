using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderService.Models;
using System.Text;
using System.Text.Json.Serialization;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IConfiguration _configuration;
        Uri _baseAddress;
        IHttpClientFactory _httpClientFactory;
        public OrderController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _baseAddress = new Uri(_configuration["ApiAddress:Inventory"]);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(Order model)
        {
            model.OrderId = Guid.NewGuid();
            // Client name (Inventory) should matach with AddHttpClient("Inventory") from Program.cs file.
            var client = _httpClientFactory.CreateClient("Inventory"); 

            int id = 2;
            var response = await client.GetAsync(_baseAddress + "/inventory/" + id);
            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            else
            {
                Exception obj = new Exception("Short Circuit Error");
                var error = JsonConvert.SerializeObject(obj);
                StringContent content = new StringContent(error,Encoding.UTF8,"application/json");
                var logClient = _httpClientFactory.CreateClient("Log");
                _baseAddress = new Uri(_configuration["ApiAddress:Log"]);
                logClient.PostAsync(_baseAddress + "/log",content);

                return StatusCode((int)response.StatusCode);
            }
        }
    }
}
