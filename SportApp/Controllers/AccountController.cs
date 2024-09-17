using Microsoft.AspNetCore.Mvc;
using SportApp.Interfaces;
using SportApp.Models;
using SportApp.Services;

namespace SportApp.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly TokenService _tokenService;
        public AccountController(IAccountService accountService, TokenService tokenService) 
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            var response = await _accountService.Login(loginDto);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            await _accountService.RegisterAsync(registerDto);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> ResetPassword()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<string>> RefreshToken(string refreshToken, int userId)
        {
            var user = _accountService.GetById(userId);

            if (!user.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refresh Token");
            }
            else if (user.TokenExpiresAt < DateTime.Now)
            {
                return Unauthorized("Token expired");
            }

            string token = _tokenService.CreateToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            _tokenService.SetRefreshToken(newRefreshToken, user);

            return Ok(token);
        }
    }
}
