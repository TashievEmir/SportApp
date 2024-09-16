using Microsoft.AspNetCore.Mvc;
using SportApp.Interfaces;
using SportApp.Models;

namespace SportApp.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService) 
        {
            _accountService = accountService;
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
    }
}
