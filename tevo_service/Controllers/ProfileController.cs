using Microsoft.AspNetCore.Mvc;
using tevo_service.Entities;
using tevo_service.Models;
using tevo_service.Models.DTOs;
using tevo_service.Services;
using System;
using System.Threading.Tasks;

namespace tevo_service.Controllers
{
    [ApiController]
    [Route("api/v1/tevo-service/[controller]/[action]")]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileService profileService;
        private readonly AppDbContext appDbContext;

        public ProfileController(ProfileService profileService, AppDbContext appDbContext)
        {
            this.profileService = profileService;
            this.appDbContext = appDbContext;
        }

        // 🔹 Get user profile info
        [HttpGet]
        public async Task<ActionResult<UserProfileDTO>> GetProfile([FromQuery] Guid userId)
        {
            var result = await profileService.GetProfileAsync(userId);
            return Ok(result);
        }

        // 🔹 Update user profile info
        [HttpPut]
        public async Task<ActionResult<string>> UpdateProfile([FromBody] ProfileUpdateModel model)
        {
            var result = await profileService.UpdateProfileAsync(model);
            return Ok(result);
        }
    }
}
