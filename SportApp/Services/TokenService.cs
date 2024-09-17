using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SportApp.Data;
using SportApp.Entities;
using SportApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
namespace SportApp.Services
{
    public class TokenService
    {
        public readonly IConfiguration _configuration;
        private readonly AppDbContext _appDbContext;
        private readonly IServiceScopeFactory _scopeFactory;
        public TokenService(IConfiguration configuration, AppDbContext appDbContext, IServiceScopeFactory serviceScopeFactory) 
        {
            _configuration = configuration;
            _appDbContext = appDbContext;
            _scopeFactory = serviceScopeFactory;
        }

        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtToken:Key").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                ExpiredAt = DateTime.Now.AddDays(7)
            };

            return refreshToken;
        }

        public void SetRefreshToken(RefreshToken newRefreshToken, User user)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.ExpiredAt
            };
            
            //Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreatedAt = newRefreshToken.CreatedAt;
            user.TokenExpiresAt = newRefreshToken.ExpiredAt;

            using (var scope = _scopeFactory.CreateScope())
            {
                var _dataContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                _dataContext.Update(user);
                _dataContext.SaveChanges();
            }
        }
    }
}
