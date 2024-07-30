using CartService.Database.Entities;
using CartService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        ICartRepository _cartRepository;
        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetUserCart(long UserId)
        {
            var cart = await _cartRepository.GetUserCart(UserId);
            return Ok(cart);
        }

        [HttpPost("{UserId}")]
        public IActionResult AddItem(CartItem item, long UserId)
        {
            var cart = _cartRepository.AddItem(UserId, item.CartId, item.ItemId, item.UnitPrice, item.Quantity);
            return Ok(cart);
        }

        [HttpGet("{CartId}")]
        public async Task<IActionResult> GetCart(int CartId)
        {
            var cart = await _cartRepository.GetCart(CartId);
            return Ok(cart);
        }

        [HttpGet("{CartId}")]
        public IActionResult GetCartItemCount(int CartId)
        {
            var count = _cartRepository.GetCartItemCount(CartId);
            return Ok(count);
        }

        [HttpGet("{CartId}")]
        public IEnumerable<CartItem> GetItems(int CartId)
        {
            return _cartRepository.GetCartItems(CartId);
        }

        [HttpGet("{CartId}")]
        public IActionResult MakeInActive(int CartId)
        {
            var status = _cartRepository.MakeInActive(CartId);
            return Ok(status);
        }

        [HttpDelete("{CartId}/{ItemId}")]
        public IActionResult DeleteItem(int CartId, int ItemId)
        {
            var count = _cartRepository.DeleteItem(CartId, ItemId);
            return Ok(count);
        }

        [HttpGet("{CartId}/{ItemId}/{Quantity}")]
        public IActionResult UpdateQuantity(int CartId, int ItemId, int Quantity)
        {
            var count = _cartRepository.UpdateQuantity(CartId, ItemId, Quantity);
            return Ok(count);
        }
    }
}
