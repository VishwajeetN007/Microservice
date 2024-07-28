using WebApp.Helpers;
using WebApp.HttpClients;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class CartController : BaseController
    {
        CartService _cartService;
        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }
        public async Task<IActionResult> Index()
        {
            if (CurrentUser == null)
            {
                return RedirectToAction("Login", "Account", new { returnUrl = "/cart" });
            }
            CartModel cartModel = await _cartService.GetUserCartAsync(CurrentUser.Id);
            return View(cartModel);
        }

        [Route("Cart/AddToCart/{ItemId}/{UnitPrice}/{Quantity}")]
        public async Task<IActionResult> AddToCart(int ItemId, decimal UnitPrice, int Quantity)
        {
            CartItemModel cartItemModel = new CartItemModel
            {
                ItemId = ItemId,
                Quantity = Quantity,
                UnitPrice = UnitPrice
            };

            CartModel cartModel = await _cartService.AddToCartAsync(cartItemModel, CurrentUser.Id);
            if (cartModel != null)
            {
                return Json(new { status = "success", count = cartModel.CartItems.Count });
            }
            return Json(new { status = "failed", count = 0 });
        }

        [Route("Cart/UpdateQuantity/{Id}/{Quantity}/{CartId}")]
        public IActionResult UpdateQuantity(int Id, int Quantity, long CartId)
        {
            int count = _cartService.UpdateQuantity(CartId, Id, Quantity).Result;
            return Json(count);
        }

        [Route("Cart/DeleteItem/{Id}/{CartId}")]
        public IActionResult DeleteItem(int Id, long CartId)
        {
            int count = _cartService.DeleteCartItemAsync(CartId, Id).Result;
            return Json(count);
        }

        [Route("Cart/GetCartCount")]
        public IActionResult GetCartCount()
        {
            if (CurrentUser != null)
            {
                var response = _cartService.GetUserCartAsync(CurrentUser.Id).Result;
                if (response != null)
                {
                    var count = response.CartItems.Count;
                    return Json(count);
                }
            }
            return Json(0);
        }

        public IActionResult CheckOut()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CheckOut(AddressModel model)
        {
            if (ModelState.IsValid)
            {
                TempData.Set("Address", model);
                return RedirectToAction("Index", "Payment");
            }
            return View();
        }
    }
}
