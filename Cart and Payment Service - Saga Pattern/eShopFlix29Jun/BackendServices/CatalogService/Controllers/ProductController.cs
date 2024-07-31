using CatalogService.Database.Entities;
using CatalogService.Models;
using CatalogService.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CatalogService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductService _productService;

        IHttpClientFactory _httpClientFactory; //For call mutiple client. (here we use for LogService)
        Uri _baseAddress;
        IConfiguration _configuration;
        public ProductController(IProductService productService, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _productService = productService;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _baseAddress = new Uri(_configuration["ApiAddress"]);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_productService.GetProducts());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_productService.GetProduct(id));
        }

        [HttpPost]
        public IActionResult Add([FromBody] Product product)
        {
            try
            {
                int x = 7, y = 0;
                int z = x / y;

                _productService.AddProduct(product);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                var model = new LogModel
                {
                    ClassName = "ProductController",
                    Message = ex.Message,
                    StackTraceString = ex.StackTrace
                };
                ////var clinet = new HttpClient(); We can also used (Generally used for one client)
                // It should match with program.cs file cleint name.(LogService)
                var client = _httpClientFactory.CreateClient("LogService"); 
                StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                _ = client.PostAsync(_baseAddress + "/Log", content).Result;
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
