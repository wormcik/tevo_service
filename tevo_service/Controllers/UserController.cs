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

        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetAllBuyer()
        {
            var result = await userService.GetAllBuyerAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Ban([FromBody] BanModel model)
        {
            var result = await userService.BanUserAsync(model);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            var result = await userService.DeleteUserAsync(id);
            if (!result)
                return NotFound("Kullanıcı bulunamadı.");
            return Ok(true);
        }

        [HttpPut]
        public async Task<ActionResult<ResultModel<UserDTO>>> UpdateUser(Guid userId, [FromBody] SignInModel model)
        {
            var result = await userService.UpdateUserAsync(userId, model);
            return Ok(result);
        }

    }
}
