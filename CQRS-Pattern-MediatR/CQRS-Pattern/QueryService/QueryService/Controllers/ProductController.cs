using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueryService.Services.Queries;

namespace QueryService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetProductsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetProductQuery { ProductId = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
