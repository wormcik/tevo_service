using tevo_service.Entities;
using tevo_service.Services;
using Microsoft.AspNetCore.Mvc;
using tevo_service.Models;
using tevo_service.Models.DTOs;

namespace tevo_service.Controllers
{
    [ApiController, Route("api/v1/tevo-service/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService authService;
        private readonly AppDbContext appDbContext;
        public AuthController(AuthService authService, AppDbContext appDbContext)
        {
            this.authService = authService;
            this.appDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> LogIn([FromBody] LogInModel model)
        {
            var result = await authService.LogInAsync(model);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> SignIn([FromBody] SignInModel model)
        {
            var result = await authService.SignInAsync(model);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> MenuPermission([FromQuery] Guid userId)
        {
            var result = await authService.CheckMenuPermissionAsync(userId);
            return Ok(result);
        }

    }
}
