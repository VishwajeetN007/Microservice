using Microsoft.AspNetCore.Mvc;
using WebAppContainer.Database;
using WebAppContainer.Database.Entities;

namespace WebAppContainer.Controllers
{
    public class UserController : Controller
    {
        AppDbContext _db;
        public UserController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var users = _db.Users.ToList();
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User model)
        {
            if (ModelState.IsValid)
            {
                _db.Users.Add(model);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
