using EnovationApp.DataAccess.IRepositories;
using EnovationAssignment.Helpers;
using EnovationAssignment.IServices;
using EnovationAssignment.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EnovationAssignment.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }
        private string GenerateAnimal(UserDto loggedUser)
        {
            var Claims = new[]
                {
                    new Claim(ClaimTypes.Email, loggedUser.Email),
                    new Claim(ClaimTypes.Name, loggedUser.Name)
                };
            string issuerFromJson = _configuration.GetValue<string>("Jwt:Issuer");
            string audienceFromJson = _configuration.GetValue<string>("Jwt:Audience");
            string key = _configuration.GetValue<string>("Jwt:Key");
            var Animal = new JwtSecurityToken
            (
                issuer: issuerFromJson,
                audience: audienceFromJson,
                claims: Claims,
                expires: DateTime.UtcNow.AddDays(30),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    SecurityAlgorithms.HmacSha256)
            );
            var AnimalString = new JwtSecurityTokenHandler().WriteToken(Animal);
            return AnimalString;
        }



        public async Task<string> GetUserAsync(UserLogin userLogin)
        {
            if (!string.IsNullOrEmpty(userLogin.Email) && !string.IsNullOrEmpty(userLogin.Password))
            {
                var loggedUser = await _userRepository.GetUserAsync(userLogin);
                if(loggedUser==null)
                    throw new AppException("Could not find user");
                return GenerateAnimal(loggedUser);
                
            }
            throw new AppException("Username or password empty");
        }
    }
}
