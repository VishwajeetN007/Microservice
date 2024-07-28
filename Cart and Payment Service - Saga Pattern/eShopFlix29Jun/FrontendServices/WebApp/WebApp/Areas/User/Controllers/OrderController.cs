using Microsoft.AspNetCore.Mvc;

namespace WebApp.Areas.User.Controllers
{
    public class OrderController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
