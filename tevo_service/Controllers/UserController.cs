using Microsoft.AspNetCore.Mvc;
using tevo_service.Entities;
using tevo_service.Models;
using tevo_service.Services;
using System.Threading.Tasks;
using tevo_service.Models.DTOs;

namespace tevo_service.Controllers
{
    [ApiController]
    [Route("api/v1/tevo-service/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;
        private readonly AppDbContext appDbContext;

        public UserController(UserService userService, AppDbContext appDbContext)
        {
            this.userService = userService;
            this.appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetAll()
        {
            var result = await userService.GetAllAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Ban([FromBody] BanModel model)
        {
            var result = await userService.BanUserAsync(model);
            return Ok(result);
        }
    }
}
