using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using WebApp.Models;

namespace WebApp.Areas.User.Controllers
{
    [Area("User")]
    public class BaseController : Controller
    {
        public UserModel CurrentUser
        {
            get
            {
                //// User is a globla property in asp.net core
             
                if (User.Claims.Count()>0)
                {
                    string userdata = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value;
                    return JsonSerializer.Deserialize<UserModel>(userdata);
                }
                return null;
            }
        }
    }
}
