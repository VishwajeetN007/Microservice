using AuthService.Models;

namespace AuthService.Repository.Interfaces
{
    public interface IAuthRepository
    {
        bool CreateUser(SignUpModel signUpModel);
        UserModel ValidateUser(LoginModel loginModel);
    }
}
