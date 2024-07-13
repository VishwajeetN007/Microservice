using InventoryService.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    { 
        [HttpGet]
        public IActionResult Get()
        {
            var data = MyData.Data;
            return Ok(data);
        }
    }
}
