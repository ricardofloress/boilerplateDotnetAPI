using baseProject.Api.Dtos;
using baseProject.Domain.Models;
using baseProject.Domain.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace baseProject.Domain.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> LoginAsync(UserDto userDto)
        {
            try
            {
                User? user = _userService.GetUserByEmail(userDto.Email);

                if (user.Equals(null))
                    throw new Exception("User not found");

                if (!verifyPasswordHash(userDto.Password, user.PasswordHash, user.PasswordSalt))
                    throw new Exception("Wrong Password!");

                string token = CreateToken(user);

                return token;
            }
            catch (Exception e)
            {
                //Do log
                throw;
            }
        }

        public Task<bool> RegisterAsync(UserDto userDto)
        {
            return _userService.AddUserAsync(userDto);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim> {
                new Claim (ClaimTypes.Email, user.Email),
                new Claim (ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("JwtConfig:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private bool verifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

    }
}
