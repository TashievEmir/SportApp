using Microsoft.AspNetCore.Mvc;

namespace SportApp.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class AccountController : ControllerBase
    {

        public AccountController() 
        {
        
        }

        [HttpPost]
        public async Task<ActionResult> Login()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Register()
        {
            return Ok();
        }

        public async Task<ActionResult> ResetPassword()
        {
            return Ok();
        }
    }
}
