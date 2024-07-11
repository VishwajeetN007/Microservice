using Microsoft.AspNetCore.Mvc;
using SupportApp.HttpClients;

namespace SupportApp.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
