using Microsoft.AspNetCore.Mvc;
using SupportApp.HttpClients;

namespace SupportApp.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAll();
            return View(products);
        }
    }
}
