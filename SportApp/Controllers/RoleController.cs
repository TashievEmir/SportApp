using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportApp.Data;
using SportApp.Entities;

namespace SportApp.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public RoleController(AppDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(_appDbContext.Roles.ToList());
        }

        [HttpPost]
        public async Task<ActionResult> Create(string roleName)
        {
            var role = new Role
            {
                Name = roleName,
            };

            _appDbContext.Roles.Add(role);
            await _appDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
