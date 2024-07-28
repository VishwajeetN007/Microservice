using SupportApp.Models;
using System.Security.Claims;
using System.Text.Json;

namespace SupportApp.HttpClients
{
    public class BaseService
    {
        IHttpContextAccessor _httpContextAccessor;

        public BaseService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public UserModel CurrentUser
        {
            get
            {
                if (_httpContextAccessor.HttpContext.User.Claims.Count() > 0)
                {
                    string userdata = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value;
                    return JsonSerializer.Deserialize<UserModel>(userdata);
                }
                return null;
            }
        }
    }
}
