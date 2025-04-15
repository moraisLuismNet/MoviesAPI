using Microsoft.IdentityModel.Tokens;
using MoviesAPI.DTOs;
using MoviesAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MoviesAPI.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly MoviesAPIDbContext _context;

        public TokenService(IConfiguration configuration, MoviesAPIDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public UserLoginResponseDTO GenerateTokenService(UserLoginDTO user)
        {
            var userDB = _context.Users.FirstOrDefault(x => x.Email == user.Email);
            if (userDB == null)
            {
                throw new UnauthorizedAccessException("User not found");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, userDB.Role)
            };

            var key = _configuration["JWTKey"];
            var keyKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signinCredentials = new SigningCredentials(keyKey, SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return new UserLoginResponseDTO
            {
                Token = tokenString,
                Email = user.Email
            };
        }
    }
}
