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

        public async Task<ResultModel<UserDTO>> UpdateUserAsync(Guid userId, SignInModel model)
        {
            var user = await appDbContext.User
                .Include(u => u.ContactInfoList)
                .Include(u => u.AddressInfoList)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
            {
                return new ResultModel<UserDTO>().Fail("Kullanıcı bulunamadı");
            }

            user.UserName = model.UserName;
            var existingContact = user.ContactInfoList.FirstOrDefault();
            if (existingContact != null)
            {
                existingContact.Type = model.ContactInfoModel.Type;
                existingContact.Value = model.ContactInfoModel.Value;
            }
            else
            {
                user.ContactInfoList.Add(new ContactInfo
                {
                    Type = model.ContactInfoModel.Type,
                    Value = model.ContactInfoModel.Value
                });
            }

            if (model.AddressInfoModel != null)
            {
                var existingAddress = user.AddressInfoList.FirstOrDefault();
                if (existingAddress != null)
                {
                    existingAddress.Type = model.AddressInfoModel.Type;
                    existingAddress.Value = model.AddressInfoModel.Value;
                    existingAddress.Latitude = model.AddressInfoModel.Latitude;
                    existingAddress.Longitude = model.AddressInfoModel.Longitude;
                }
                else
                {
                    user.AddressInfoList.Add(new AddressInfo
                    {
                        Type = model.AddressInfoModel.Type,
                        Value = model.AddressInfoModel.Value,
                        Latitude = model.AddressInfoModel.Latitude,
                        Longitude = model.AddressInfoModel.Longitude
                    });
                }
            }


            await appDbContext.SaveChangesAsync();

            return new ResultModel<UserDTO>().Success(new UserDTO
            {
                UserId = user.UserId,
                UserName = user.UserName
            });
        }


    }
}
