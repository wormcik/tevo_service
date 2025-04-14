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

        public async Task<List<UserBuyerDTO>> GetAllBuyerAsync()
        {
            return await appDbContext.User
                .Include(q => q.AddressInfoList)
                .Include(q => q.ContactInfoList)
                .Where(u => u.IsBanned == null || u.IsBanned == false)
                .Select(u => new UserBuyerDTO
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    Role = u.Role,
                    ContactInfoList = u.ContactInfoList.Select(ci => new ContactInfoDTO
                    {
                        ContactInfoId = ci.ContactInfoId,
                        Type = ci.Type,
                        Value = ci.Value
                    }).ToList(),
                    AddressInfoList = u.AddressInfoList.Select(ai => new AddressInfoDTO
                    {
                        AddressInfoId = ai.AddressInfoId,
                        Type = ai.Type,
                        Value = ai.Value,
                        Latitude = ai.Latitude,
                        Longitude = ai.Longitude
                    }).ToList()
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

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            var user = await appDbContext.User
                .Include(u => u.ContactInfoList)
                .Include(u => u.AddressInfoList)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
                return false;

            appDbContext.ContactInfo.RemoveRange(user.ContactInfoList);
            appDbContext.AddressInfo.RemoveRange(user.AddressInfoList);
            appDbContext.User.Remove(user);

            await appDbContext.SaveChangesAsync();
            return true;
        }

    }
}
