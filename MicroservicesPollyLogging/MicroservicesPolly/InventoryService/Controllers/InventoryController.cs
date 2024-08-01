using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult GetInventory(int id)
        {
            // TO DO:
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
