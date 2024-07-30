using Microsoft.AspNetCore.Mvc.Razor;
using System.Security.Claims;
using System.Text.Json;
using WebApp.Models;

namespace WebApp.Helpers
{
    public abstract class BaseViewPage<TModel>: RazorPage<TModel>
    {
        public UserModel CurrentUser
        {
            get
            {
                // Get the ClaimsPrincipal of the current logged in user.
                if (User.Claims.Count() > 0)
                {
                    string userdata = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value;
                    return JsonSerializer.Deserialize<UserModel>(userdata);
                }
                return null;
            }
        }
    }
}
