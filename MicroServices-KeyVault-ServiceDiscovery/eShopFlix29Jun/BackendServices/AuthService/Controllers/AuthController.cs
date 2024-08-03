using AuthService.Models;
using AuthService.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            try
            {
                UserModel user = _authRepository.ValidateUser(model);
                if (user != null)
                {
                    return Ok(user);
                }
                return BadRequest("Invalid credentials");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult SignUp(SignUpModel model)
        {
            try
            {
                bool result = _authRepository.CreateUser(model);
                if (result)
                {
                    return Ok("User created successfully");
                }
                return BadRequest("User creation failed");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public IEnumerable<UserModel> GetAll()
        {
            return _authRepository.GetUsers();
        }
    }
}
