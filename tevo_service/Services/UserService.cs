using Microsoft.EntityFrameworkCore;
using tevo_service.Entities;
using tevo_service.Models;
using tevo_service.Models.DTOs;

namespace tevo_service.Services
{
    public class UserService
    {
        private readonly AppDbContext appDbContext;

        public UserService(AppDbContext context)
        {
            appDbContext = context;
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            return await appDbContext.User
                .Where(u => u.IsBanned == null || u.IsBanned == false) 
                .Select(u => new UserDTO
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    Role = u.Role,
                })
                .ToListAsync();
        }

        public async Task<bool> BanUserAsync(BanModel model)
        {
            var user = await appDbContext.User.FindAsync(model.UserId);

            if (user == null)
                return false;

            user.IsBanned = true;
            user.BanReason = model.BanNote ?? "";

            await appDbContext.SaveChangesAsync();
            return true;
        }
    }
}
