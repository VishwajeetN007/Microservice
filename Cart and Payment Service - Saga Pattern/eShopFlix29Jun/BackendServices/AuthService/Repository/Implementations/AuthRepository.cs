using AuthService.Database;
using AuthService.Database.Entities;
using AuthService.Models;
using AuthService.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.Repository.Implementations
{
    public class AuthRepository : IAuthRepository
    {
        AppDbContext _db;
        IConfiguration _configuration;

        public AuthRepository(IConfiguration configuration, AppDbContext db)
        {
            _db = db;
            _configuration = configuration;
        }
        public bool CreateUser(SignUpModel signUpModel)
        {
            Role userRole = _db.Roles.FirstOrDefault(x => x.Name == signUpModel.Role);
            if (userRole != null)
            {
                User user = new User()
                {
                    Name = signUpModel.Name,
                    Email = signUpModel.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(signUpModel.Password),
                    EmailConfirmed = false,
                    PhoneNumber = signUpModel.PhoneNumber,
                    CreatedDate = DateTime.Now
                };
                user.Roles.Add(userRole);
                _db.Users.Add(user);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public UserModel ValidateUser(LoginModel loginModel)
        {
            User user = _db.Users.Include(u => u.Roles).FirstOrDefault(x => x.Email == loginModel.Email);
            if (user != null)
            {
                bool isValid = BCrypt.Net.BCrypt.Verify(loginModel.Password, user.Password);
                if (isValid)
                {
                    UserModel userModel = new UserModel()
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Roles = user.Roles.Select(x => x.Name).ToArray()
                    };
                    userModel.Token = GenerateJwtToken(userModel);
                    return userModel;
                }
            }
            return null;
        }

        private string GenerateJwtToken(UserModel userModel)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userModel.Name),
                new Claim(JwtRegisteredClaimNames.Email, userModel.Email), // Email claim, use for Authentication.
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Roles", string.Join(",",userModel.Roles)) // Custom Roles Claim, use for Authorization.
            };
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
