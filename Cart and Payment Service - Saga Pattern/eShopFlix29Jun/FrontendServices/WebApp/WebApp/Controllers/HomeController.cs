using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Helpers;
using WebApp.HttpClients;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        private readonly CatalogService _catalogService;

        public HomeController(CatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        public async Task<IActionResult> Index()
        {

            /*
            try
            {
                int x = 6, y = 0;
                var z = x / y;
            }
            catch (Exception ex)
            {
                LogService.LogError(ex);
            }
            */

            var products = await _catalogService.GetProducts();
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
