using Microsoft.EntityFrameworkCore;
using tevo_service.Entities;
using tevo_service.Models;
using tevo_service.Models.DTOs;

namespace tevo_service.Services
{
    public class ProfileService
    {
        private readonly AppDbContext appDbContext;

        public ProfileService(AppDbContext context)
        {
            appDbContext = context;
        }
        public async Task<ResultModel<UserProfileDTO>> GetProfileAsync(Guid userId)
        {
            var user = await appDbContext.User
                .Include(u => u.ContactInfoList)
                .Include(u => u.AddressInfoList)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
                return new ResultModel<UserProfileDTO>().Fail("Kullanıcı bulunamadı");

            var dto = new UserProfileDTO
            {
                UserId = user.UserId,
                UserName = user.UserName,
                ContactInfoList = user.ContactInfoList?.Select(c => new ContactInfoDTO
                {
                    ContactInfoId = c.ContactInfoId,
                    Type = c.Type,
                    Value = c.Value
                }).ToList(),
                AddressInfoList = user.AddressInfoList?.Select(a => new AddressInfoDTO
                {
                    AddressInfoId = a.AddressInfoId,
                    Type = a.Type,
                    Value = a.Value,
                    Latitude = a.Latitude,
                    Longitude = a.Longitude
                }).ToList()
            };

            return new ResultModel<UserProfileDTO>().Success(dto);
        }

        public async Task<ResultModel<string>> UpdateProfileAsync(ProfileUpdateModel model)
        {
            var user = await appDbContext.User
                .Include(u => u.ContactInfoList)
                .Include(u => u.AddressInfoList)
                .FirstOrDefaultAsync(u => u.UserId == model.UserId);

            if (user == null)
                return new ResultModel<string>().Fail("Kullanıcı bulunamadı");

            user.UserName = model.UserName ?? user.UserName;

            if (!string.IsNullOrWhiteSpace(model.Password))
                user.Password = Encrypt(model.Password);

            // 🔄 Update ContactInfos
            appDbContext.ContactInfo.RemoveRange(user.ContactInfoList ?? []);
            if (model.ContactInfoList?.Any() == true)
            {
                foreach (var contact in model.ContactInfoList)
                {
                    appDbContext.ContactInfo.Add(new ContactInfo
                    {
                        UserId = user.UserId,
                        Type = contact.Type,
                        Value = contact.Value
                    });
                }
            }

            // 🔄 Update AddressInfos
            appDbContext.AddressInfo.RemoveRange(user.AddressInfoList ?? []);
            if (model.AddressInfoList?.Any() == true)
            {
                foreach (var address in model.AddressInfoList)
                {
                    appDbContext.AddressInfo.Add(new AddressInfo
                    {
                        UserId = user.UserId,
                        Type = address.Type,
                        Value = address.Value,
                        Latitude = address.Latitude,
                        Longitude = address.Longitude
                    });
                }
            }

            await appDbContext.SaveChangesAsync();
            return new ResultModel<string>().Success("Profil başarıyla güncellendi");
        }

        private string Encrypt(string password)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}
