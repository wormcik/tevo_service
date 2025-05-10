using Microsoft.EntityFrameworkCore;
using System.Text;
using tevo_service.Entities;
using tevo_service.Models;
using tevo_service.Models.DTOs;
using System.Security.Cryptography;

namespace tevo_service.Services
{
    public class AuthService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConfiguration configuration;
        public AuthService(IConfiguration configuration, AppDbContext appDbContext)
        {
            this.configuration = configuration;
            this.appDbContext = appDbContext;
        }
        public async Task<ResultModel<UserDTO>> LogInAsync(LogInModel model)
        {
            var dbRes = await appDbContext.User.SingleOrDefaultAsync(q => q.UserName == model.UserName);
            if (dbRes == null)
            {
                return new ResultModel<UserDTO>().Fail("Kullanıcı Bulunamadı, Kayıt Olunuz");
            }
            var decryptedPassword = Decrypt(dbRes.Password);
            if (decryptedPassword != model.Password)
            {
                return new ResultModel<UserDTO>().Fail("şifre Hatalıdır");
            }
            if (dbRes.IsBanned == true)
            {
                return new ResultModel<UserDTO>().Fail("Kullanıcı yasaklıdır");
            }
            return new ResultModel<UserDTO>().Success(new UserDTO
            {
                UserId = dbRes.UserId,
                UserName = dbRes.UserName,
            });
        }

        public async Task<ResultModel<UserDTO>> SignInAsync(SignInModel model)
        {
            var exists = await appDbContext.User.AnyAsync(u => u.UserName == model.UserName);
            if (exists)
            {
                return new ResultModel<UserDTO>().Fail("Bu kullanıcı adı kullanılmaktadır");
            }

            if (model.Password == null)
            {
                return new ResultModel<UserDTO>().Fail("Şifre Girilmelidir");
            }

            var encryptedPassword = Encrypt(model.Password);

            var user = new User
            {
                UserName = model.UserName,
                Password = encryptedPassword,
                Role = model.Role,
                IsBanned = false,
                ContactInfoList = new List<ContactInfo>(),
                AddressInfoList = new List<AddressInfo>()
            };

            if (model.ContactInfoModel != null)
            {
                var contactInfo = new ContactInfo()
                {
                    Type = model.ContactInfoModel.Type,
                    Value = model.ContactInfoModel.Value,
                };
                user.ContactInfoList.Add(contactInfo);
            }

            if (model.AddressInfoModel != null)
            {
                var addressInfo = new AddressInfo()
                {
                    Type = model.AddressInfoModel.Type,
                    Value = model.AddressInfoModel.Value,
                    Latitude = model.AddressInfoModel.Latitude,
                    Longitude = model.AddressInfoModel.Longitude,
                };
                user.AddressInfoList.Add(addressInfo);
            }

            appDbContext.User.Add(user);
            await appDbContext.SaveChangesAsync();

            return new ResultModel<UserDTO>().Success(new UserDTO
            {
                UserId = user.UserId,
                UserName = user.UserName
            });
        }

        public async Task<ResultModel<string>> CheckMenuPermissionAsync(Guid userId)
        {
            var user = await appDbContext.User.FindAsync(userId);

            if (user == null)
                return new ResultModel<string>().Fail("Kullanıcı bulunamadı");

            return new ResultModel<string>().Success(user.Role);
        }

        private byte[] GetAesKey()
        {
            var rawKey = configuration["Encryption:Key"];
            using var sha = SHA256.Create();
            return sha.ComputeHash(Encoding.UTF8.GetBytes(rawKey)); // 32 byte key
        }

        private string Encrypt(string plainText)
        {
            var keyBytes = GetAesKey();

            using var aes = Aes.Create();
            aes.Key = keyBytes;
            aes.GenerateIV();
            var iv = aes.IV;

            using var encryptor = aes.CreateEncryptor(aes.Key, iv);
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            var encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

            var result = new byte[iv.Length + encryptedBytes.Length];
            Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
            Buffer.BlockCopy(encryptedBytes, 0, result, iv.Length, encryptedBytes.Length);

            return Convert.ToBase64String(result);
        }

        private string Decrypt(string encryptedText)
        {
            var keyBytes = GetAesKey();

            var fullCipher = Convert.FromBase64String(encryptedText);
            using var aes = Aes.Create();
            aes.Key = keyBytes;

            var iv = new byte[16];
            var cipher = new byte[fullCipher.Length - iv.Length];
            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, cipher.Length);

            using var decryptor = aes.CreateDecryptor(aes.Key, iv);
            var decryptedBytes = decryptor.TransformFinalBlock(cipher, 0, cipher.Length);
            return Encoding.UTF8.GetString(decryptedBytes);
        }


    }
}
