using Microsoft.AspNetCore.Mvc.Razor;
using SupportApp.Models;
using System.Security.Claims;
using System.Text.Json;

namespace SupportApp.Helpers
{
    public abstract class BaseViewPage<TModel> : RazorPage<TModel>
    {
        public UserModel CurrentUser
        {
            get
            {
                //Get the ClaimsPrincipal for user associated with the executing action.
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
